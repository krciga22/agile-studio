import React, {useEffect, useState} from 'react';
import api from "../../../services/api/Api.tsx";
import './Filters.css';
import type {FilterValue} from "./Filters.tsx";

type Props = {
  name: string; // filter key
  endpoint: string; // API endpoint to load options from (e.g. '/Project/owners')
  value?: FilterValue;
  setValue: (name: string, value: FilterValue) => void;
  label?: string;
  valueField?: string; // defaults to 'id'
  labelField?: string; // defaults to 'title' or 'name'
  placeholder?: string;
}

export default function MultiSelectFilter({
  name,
  endpoint,
  value,
  setValue,
  label,
  valueField = 'id',
  labelField = 'title',
  placeholder = 'Filter...'
}: Props){
  const [options, setOptions] = useState<FilterValue[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    let mounted = true;
    const load = async () => {
      setIsLoading(true);
      try{
        const resp = await api.get(endpoint);
        const data = resp.data as unknown;
        if(!mounted) return;
        // Accept API responses that are either the new shape [{ value, label }] or
        // old-style objects where we should read valueField/labelField (or id/name/title)
        const arr = Array.isArray(data) ? data as Array<unknown> : [];
        const mapped = arr.map(d => {
          const obj = d as Record<string, unknown> | null;
          // If the item already matches { value: unknown, label: string }
          if(obj && ('value' in obj) && typeof obj['label'] === 'string'){
            return { value: obj['value'] ?? '', label: String(obj['label']) } as FilterValue;
          }

          // Fallback to previous behavior: read valueField/labelField or common fallbacks
          const val = (obj && ((obj[valueField] ?? obj['id']))) ?? '';
          const lab = (obj && ((obj[labelField] ?? obj['name'] ?? obj['title']))) ?? String(val);
          return { value: val, label: String(lab) } as FilterValue;
        });
        setOptions(mapped as FilterValue[]);
      }
      catch(err){
        console.error('Error loading filter options from', endpoint, err);
        setOptions([]);
      }
      finally{
        if(mounted) setIsLoading(false);
      }
    }

    load();

    return () => { mounted = false; }
  }, [endpoint, valueField, labelField]);

  const onSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (e) => {

    const values: Array<unknown> = [];
    const labels: Array<string> = [];

    Array.from(e.target.selectedOptions).forEach(o => {
      values.push(o.value);
      labels.push(o.label);
    });

    setValue(name, {
      value: values,
      label: labels
    });
  }

  return (
    <div className="DataTable-filter">
      {label && <label className="form-label me-2">{label}</label>}

      <select
        multiple
        className="form-select"
        value={value ? (value?.value as number[]).map(v => String(v)) : []}
        onChange={onSelectChange}
        aria-label={label ?? name}
      >
        {isLoading && <option disabled>Loading...</option>}
        {!isLoading && options.length === 0 && <option disabled>{placeholder}</option>}
        {options.map(opt => (
          <option key={String(opt.value)} value={String(opt.value)}>
            {opt.label}
          </option>
        ))}
      </select>
    </div>
  );
}
