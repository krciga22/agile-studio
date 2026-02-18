import React from 'react';
import './Modal.css';

type Props = {
  isOpen: boolean;
  onClose?: () => void;
  title?: string;
  children?: React.ReactNode;
  className?: string;
}

export default function Modal({ isOpen, onClose, title, children, className }: Props) {
  if (!isOpen) return null;

  return (
    <div className={['modal-overlay'].join(' ')} onMouseDown={onClose}>
      <div className={['modal-content', className].filter(Boolean).join(' ')} onMouseDown={e => e.stopPropagation()}>
        {title && <div className="modal-header"><h3>{title}</h3></div>}
        <div className="modal-body">{children}</div>
      </div>
    </div>
  );
}
