import React, {useContext, useMemo} from "react";
import {DataTableContext} from "./DataTableContext.tsx";

export type PaginationDetails = {
  pageSize: number,
  currentPage: number,
  totalPages: number,
};

type PaginationProps = {
  setPage: (page: number) => Promise<void>|void;
}

export default function Pagination({ setPage }: PaginationProps) {
  const ctx = useContext(DataTableContext);
  const paginationDetails = ctx.paginationDetails as PaginationDetails | null;

  const currentPage = paginationDetails?.currentPage ?? 1;
  const totalPages = paginationDetails?.totalPages ?? 0;

  const pages = useMemo(() => {
    const total = Math.max(0, Math.floor(totalPages));
    const pagesList: number[] = [];

    if (total <= 0) return pagesList;

    const maxButtons = 9;
    if (total <= maxButtons) {
      for (let i = 1; i <= total; i++) pagesList.push(i);
      return pagesList;
    }

    // pages either side of current
    const side = 2;
    const start = Math.max(2, currentPage - side);
    const end = Math.min(total - 1, currentPage + side);

    pagesList.push(1);
    if (start > 2) pagesList.push(-1);
    for (let i = start; i <= end; i++) pagesList.push(i);
    if (end < total - 1) pagesList.push(-1);
    pagesList.push(total);

    return pagesList;
  }, [currentPage, totalPages]);

  if (!paginationDetails) return null;

  if (totalPages <= 1) return null;

  const updatePage = (newPage: number) => {
    setPage(newPage);
  }

  const onClickPage = (e: React.MouseEvent, p: number) => {
    e.preventDefault();
    if (p === currentPage) return;
    updatePage(p);
  }

  const onClickPrev = (e: React.MouseEvent) => {
    e.preventDefault();
    if (currentPage <= 1) return;
    updatePage(currentPage - 1);
  }

  const onClickNext = (e: React.MouseEvent) => {
    e.preventDefault();
    if (currentPage >= totalPages) return;
    updatePage(currentPage + 1);
  }

  return (
    <nav className="DataTable-pagination" aria-label="Pagination">
      <ul className="pagination">
        <li className={"page-item " + (currentPage <= 1 ? 'disabled' : '')}>
          <a href="#" className="page-link" onClick={onClickPrev}>Previous</a>
        </li>

        {pages.map((p, idx) => (
          <li key={"p_" + idx} className={"page-item " + ((p === currentPage) ? 'active' : '')}>
            {p === -1 ? (
              <span className="page-link">&hellip;</span>
            ) : (
              <a href="#" className="page-link" onClick={(e) => onClickPage(e, p)}>{p}</a>
            )}
          </li>
        ))}

        <li className={"page-item " + (currentPage >= totalPages ? 'disabled' : '')}>
          <a href="#" className="page-link" onClick={onClickNext}>Next</a>
        </li>
      </ul>
    </nav>
  );
}
