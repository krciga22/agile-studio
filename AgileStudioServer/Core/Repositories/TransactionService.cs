using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace AgileStudioServer.Core.Data
{
    /// <summary>
    /// Manages database transactions across multiple repositories.
    /// </summary>
    public class TransactionService : IDisposable
    {
        private readonly DBContext _dbContext;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        public TransactionService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Executes an action within a transaction scope.
        /// </summary>
        public T ExecuteInTransaction<T>(Func<T> operation)
        {
            if (_dbContext.Database.CurrentTransaction != null){
                return operation();
            }

            BeginTransaction();
            try
            {
                var result = operation();
                Commit();
                return result;
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        /// <summary>
        /// Executes an action within a transaction scope (void return).
        /// </summary>
        public void ExecuteInTransaction(Action operation)
        {
            if (_dbContext.Database.CurrentTransaction != null){
                operation();
            }

            BeginTransaction();
            try
            {
                operation();
                Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        private void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Commits the current transaction.
        /// </summary>
        private void Commit()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            try
            {
                _dbContext.SaveChanges();
                _transaction.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        /// <summary>
        /// Rolls back the current transaction.
        /// </summary>
        private void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                DisposeTransaction();
            }
        }

        private void DisposeTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _transaction?.Dispose();
                _disposed = true;
            }
        }
    }
}