import {useMemo, useState, useContext} from 'react';
import Modal from '../modal/Modal';
import { DataTableContext } from './DataTableContext.tsx';

type SortOption = {
  fieldKey: string;
  label: string;
}

type SortRow = {
  id: number;
  fieldKey: string;
  direction: 'asc' | 'desc';
}

type Props = {
  sortableFields: SortOption[];
  setSort: (sort: string[]) => void;
}

export default function Sort({ sortableFields, setSort }: Props) {
  const ctx = useContext(DataTableContext);
  const [isOpen, setIsOpen] = useState(false);
  const [rows, setRows] = useState<SortRow[]>([]);
  const [error, setError] = useState<string|null>(null);
  const nextId = useMemo(() => {
    let n = 1;
    return () => n++;
  }, []);

  const openModal = () => {
    let initialRows: SortRow[] = [];

    const currentSort = ctx?.sort ?? [];
    if(currentSort.length > 0){
      const parsedRows: SortRow[] = currentSort.map(s => {
        const parts = s.split(':');
        const fieldKey = parts[0] ?? '';
        const dir = (parts[1] === 'desc') ? 'desc' : 'asc';
        return {
          id: nextId(),
          fieldKey,
          direction: dir
        };
      })

      initialRows = parsedRows.filter(p =>
        sortableFields.some(f => f.fieldKey === p.fieldKey));
    }

    if(initialRows.length === 0 && sortableFields.length > 0){
      initialRows.push({
        id: nextId(),
        fieldKey: '',
        direction: 'asc'
      });
    }

    if(initialRows.length > 0){
      setRows(initialRows);
    }

    setError(null);
    setIsOpen(true);
  }

  const addRow = () => {
    setError(null);
    setRows(prev => [...prev, { id: nextId(), fieldKey: '', direction: 'asc' }]);
  }

  const updateRowField = (id: number, fieldKey: string) => {
    setError(null);
    setRows(prev => prev.map(r => r.id === id ? { ...r, fieldKey } : r));
  }

  const updateRowDirection = (id: number, direction: 'asc' | 'desc') => {
    setError(null);
    setRows(prev => prev.map(r => r.id === id ? { ...r, direction } : r));
  }

  const removeRow = (id: number) => {
    setError(null);
    setRows(prev => prev.filter(r => r.id !== id));
  }

  const isRowValid = (row: SortRow) => {
    if(!row.fieldKey) return false;
    return sortableFields.some(f => f.fieldKey === row.fieldKey);
  }

  const isRowDuplicated = (row: SortRow) => {
    return rows.filter(r => r.fieldKey === row.fieldKey).length > 1;
  }

  const applySort = () => {
    const invalid = rows.find(r => !isRowValid(r));
    if(invalid){
      setError('One or more selected fields are invalid.');
      return;
    }

    const duplicated = rows.find(r => isRowDuplicated(r));
    if(duplicated){
      setError('All selected fields must be unique.');
      return;
    }

    const sortArray = rows.map(r => `${r.fieldKey}:${r.direction}`);
    setSort(sortArray);
    setError(null);
    setIsOpen(false);
  }

  return (
    <>
      <button
        type="button"
        className="btn btn-secondary"
        onClick={openModal}
        disabled={sortableFields.length === 0}
      >
        Sort
      </button>

      <Modal isOpen={isOpen} onClose={() => setIsOpen(false)} title={"Sort"}>
        <div className="SortModal">
          {sortableFields.length === 0 ? (
            <div>No sortable fields available.</div>
          ) : (
            <div className={"container g-0"}>
              {rows.map(row => (
                <div key={row.id} className="row mb-2">
                  <div className={"col-6"}>
                    <select
                      className="form-select me-2"
                      value={row.fieldKey}
                      onChange={e => updateRowField(row.id, e.target.value)}
                    >
                      <option key={'default'} value={''}></option>

                      {sortableFields.map(opt => (
                        <option key={opt.fieldKey} value={opt.fieldKey}>{opt.label}</option>
                      ))}
                    </select>
                  </div>

                  <div className={"col-4"}>
                    <select
                      className="form-select me-2"
                      value={row.direction}
                      onChange={e => updateRowDirection(row.id, e.target.value as 'asc' | 'desc')}
                    >
                      <option value="asc">Ascending</option>
                      <option value="desc">Descending</option>
                    </select>
                  </div>

                  <div className={"col-2"}>
                    <button type="button" className="btn btn-outline-danger btn-sm" onClick={() => removeRow(row.id)}>Remove</button>
                  </div>
                </div>
              ))}

              <div className="mb-3">
                <button type="button" className="btn btn-link" onClick={addRow} disabled={rows.length >= sortableFields.length}>+ Add field</button>
              </div>

              <div className="d-flex justify-content-between">
                <div>
                  {error && <div className="text-danger mb-2">{error}</div>}
                </div>

                <div className="d-flex justify-content-end">
                  <button type="button" className="btn btn-secondary me-2" onClick={() => setIsOpen(false)}>Cancel</button>
                  <button type="button" className="btn btn-primary" onClick={applySort}>Sort</button>
                </div>
              </div>
            </div>
          )}
        </div>
      </Modal>
    </>
  );
}
