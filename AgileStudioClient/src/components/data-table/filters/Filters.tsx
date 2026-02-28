import React, {useMemo, useState, useContext} from 'react';
import Modal from '../../modal/Modal';
import './Filters.css';
import {faFilter} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {DataTableContext} from "../DataTableContext.tsx";

export type FilterValue = { value: unknown; label: string | string[] };

type Props = {
  onCancel: () => void;
  onApply: () => void;
  onClear: () => void;
  children?: React.ReactNode;
}

export default function Filters({ onCancel, onApply, onClear, children }: Props){
  const ctx = useContext(DataTableContext);
  const [isOpen, setIsOpen] = useState(false);

  const activeCount = useMemo(() => {
    return Object.keys(ctx.filters || {}).length
  }, [ctx.filters]);

  const clearAll = () => {
    onClear();
    setIsOpen(false);
  }

  const apply = () => {
    onApply();
    setIsOpen(false);
  }

  const cancel = () => {
    onCancel();
    setIsOpen(false);
  };

  const openBtnClassName = ['btn'];
  if(activeCount > 0){
    openBtnClassName.push('btn-secondary');
  }
  else{
    openBtnClassName.push('btn-outline-secondary');
  }

  return (
    <div className="DataTable-Filters">
      <button
        type="button"
        className={openBtnClassName.join(' ')}
        onClick={() => setIsOpen(true)}
        aria-label="Open filters"
      >
        <FontAwesomeIcon icon={faFilter} /> {activeCount > 0 ? ` (${activeCount})` : ''}
      </button>

      <Modal isOpen={isOpen} onClose={() => setIsOpen(false)} title={"Filters"}>
        <div className="FiltersModal">
          {children ? children : <div>No filters available.</div>}

          <div className="d-flex justify-content-end mt-3">
            <button type="button" className="btn btn-secondary me-2" onClick={cancel}>Cancel</button>
            <button type="button" className="btn btn-primary me-2" onClick={apply}>Apply</button>
            {
              activeCount > 0 &&
                <button type="button" className="btn btn-link" onClick={clearAll}>Clear</button>
            }
          </div>
        </div>
      </Modal>
    </div>
  );
}
