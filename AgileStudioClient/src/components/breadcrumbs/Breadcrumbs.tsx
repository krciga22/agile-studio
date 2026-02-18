import React from 'react';
import './Breadcrumbs.css';

type BreadcrumbProps = {
  children: React.ReactNode;
  href?: string;
  onClick?: (e: React.MouseEvent) => void;
  className?: string;
}

export function Breadcrumb({ children, href, onClick, className }: BreadcrumbProps) {
  const handleKeyDown = (e: React.KeyboardEvent) => {
    // activate on Enter or Space
    if (onClick && (e.key === 'Enter' || e.key === ' ')) {
      // forward the keyboard event as a mouse event-compatible type
      onClick((e as unknown) as React.MouseEvent);
    }
  };

  const isClickable = Boolean(href || onClick);
  const innerClassName = [isClickable ? 'clickable' : '', className].filter(Boolean).join(' ') || undefined;

  if (href) {
    return (
      <li className={className ?? ''}>
        <a className={innerClassName} href={href} onClick={onClick}>{children}</a>
      </li>
    );
  }

  return (
    <li className={className ?? ''}>
      <span
        className={innerClassName}
        role={onClick ? 'button' : undefined}
        tabIndex={onClick ? 0 : undefined}
        onClick={onClick}
        onKeyDown={onClick ? handleKeyDown : undefined}
      >
        {children}
      </span>
    </li>
  );
}

type BreadcrumbsProps = {
  children: React.ReactNode;
  className?: string;
}

export default function Breadcrumbs({ children, className }: BreadcrumbsProps) {
  return (
    <ul className={['breadcrumbs', className].filter(Boolean).join(' ')}>
      {children}
    </ul>
  );
}
