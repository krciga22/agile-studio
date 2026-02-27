import React, {useEffect, useRef, useState} from "react";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSearch} from "@fortawesome/free-solid-svg-icons";
import './Search.css';

type SearchProps = {
  setSearchQuery: (query: string) => void;
  placeholder?: string;
}

export default function Search({ setSearchQuery, placeholder = 'Search...' }: SearchProps) {
  const [visible, setVisible] = useState<boolean>(false);
  const [value, setValue] = useState<string>('');
  const inputRef = useRef<HTMLInputElement | null>(null);

  useEffect(() => {
    if (visible && inputRef.current) {
      inputRef.current.focus();
      inputRef.current.select();
    }
  }, [visible]);

  const doSearch = (val:string|null = null) => {
    const q = val !== null ? val.trim() : value.trim();
    setSearchQuery(q);
  }

  const onKeyDown: React.KeyboardEventHandler<HTMLInputElement> = (e) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      doSearch();
    }
    else if (e.key === 'Escape') {
      setVisible(false);
      setValue('');
      doSearch('');
    }
  }

  const onBlur: React.FocusEventHandler<HTMLInputElement> = () => {
    if(visible){
      if(value.length === 0){
        setVisible(false);
      }
    }
  }

  return (
    <div className="DataTable-search d-flex align-items-center">
      {visible && (
        <div className="DataTable-search-input ms-2 d-flex align-items-center">
          <input
            ref={inputRef}
            className="form-control"
            placeholder={placeholder}
            value={value}
            onChange={(e) => setValue(e.target.value)}
            onKeyDown={onKeyDown}
            onBlur={onBlur}
            aria-label="Search input"
          />
        </div>
      )}

      {!visible && (
        <button
          type="button"
          className="btn btn-outline-secondary"
          onClick={() => setVisible(v => !v)}
          aria-label="Toggle search"
        >
          <FontAwesomeIcon icon={faSearch} />
        </button>
      )}
    </div>
  );
}
