import React, {useContext} from "react";
import './DataTable.css'
import {DataTableContext} from "./DataTableContext.tsx";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";

export type DataTableColumn<T> = {
  key: string; // unique column key
  header?: React.ReactNode;
  field?: keyof T; // optional shorthand to display a field
  render?: (item: T) => React.ReactNode; // custom renderer
  className?: string;
  width?: string;
}

export type DataTableItem = {
  id?: number;
};

type DataTableProps<T> = {
  columns: DataTableColumn<T>[];
  isLoading?: boolean;
  emptyMessage?: string;
  onRowClick?: (item:T) => void;
  rowKey?: (item:T) => string | number;
  tableClassName?: string; // allow additional classes (eg: 'table-hover')
}

function DataTableInner<T>({
  columns,
  isLoading,
  emptyMessage = 'No records found.',
  onRowClick,
  rowKey,
  tableClassName = 'table table-hover mb-0',
}: DataTableProps<T>) {

  const ctx = useContext(DataTableContext);
  const data = ctx.data as T[];

  const defaultRowKey = (item: DataTableItem): number => {
    const id = item['id'];
    if(typeof id !== 'number'){
      throw new Error(
        'DataTable: Default row key requires items to have a numeric "id" field. ' +
        'Please provide a custom rowKey function or ensure your data items have an "id" field of type number.'
      );
    }

    return id;
  }

  return (
    <div className="DataTable table-responsive">
      { isLoading === undefined && <></> }

      {
        isLoading === true &&
          <div className="DataTable-loading">
              <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true} className={"me-2"}></FontAwesomeIcon>
              Loading...
          </div>
      }

      {
        isLoading === false &&
        <table className={tableClassName}>
            <thead>
            <tr>
              {columns.map(col => (
                <th key={col.key} style={col.width ? {width: col.width} : undefined} className={col.className ?? ''}>
                  {col.header}
                </th>
              ))}
            </tr>
            </thead>
            <tbody>
            {data.length === 0 ? (
              <tr>
                <td colSpan={columns.length} className="DataTable-empty">
                  {emptyMessage}
                </td>
              </tr>
            ) : (
              data.map(item => (
                <tr
                  key={rowKey ? rowKey(item) : defaultRowKey(item as DataTableItem)}
                  className={onRowClick ? 'DataTable-row-clickable' : ''}
                  onClick={onRowClick ? () => onRowClick(item) : undefined}
                >
                  {columns.map(col => (
                    <td key={col.key} className={col.className ?? ''}>
                      {col.render ? col.render(item) : (col.field ? (item[col.field] as unknown as React.ReactNode) : null)}
                    </td>
                  ))}
                </tr>
              ))
            )}
            </tbody>
        </table>
      }
    </div>
  );
}

export default DataTableInner;
