import './AccountMenu.css'
import { useContext, useEffect, useRef, useState } from "react";
import type { AccountDto } from "../../../services/api/dtos/AccountDtos.tsx";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import { getAccounts } from "../../../services/api/endpoints/accounts/Accounts.tsx";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";

type AccountMenuProps = {
  onSelect?: (a: AccountDto | null) => void
};

function AccountMenu({ onSelect }: AccountMenuProps) {
  const currentUser = useContext(CurrentUserContext);
  const [isRefreshing, setIsRefreshing] = useState<boolean | null>(null);
  const [accounts, setAccounts] = useState<AccountDto[]>([]);
  const [selectedAccount, setSelectedAccount] = useState<AccountDto | null>(null);
  const [open, setOpen] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const rootRef = useRef<HTMLDivElement | null>(null);



  // Clear when no user
  useEffect(() => {
    if (!currentUser.isLoading && !currentUser.user) {
      setAccounts([]);
      setSelectedAccount(null);
      setError(null);
      setIsRefreshing(false);
      if (onSelect) onSelect(null);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [currentUser.isLoading, currentUser.user]);

  useEffect(() => {
    function handleOutside(e: MouseEvent) {
      if (!rootRef.current) return;
      if (!rootRef.current.contains(e.target as Node)) setOpen(false);
    }
    function handleKey(e: KeyboardEvent) {
      if (e.key === "Escape") setOpen(false);
    }
    document.addEventListener("mousedown", handleOutside);
    document.addEventListener("keydown", handleKey);
    return () => {
      document.removeEventListener("mousedown", handleOutside);
      document.removeEventListener("keydown", handleKey);
    };
  }, []);

  const refresh = () => {
    if (isRefreshing) return;
    setIsRefreshing(true);
    setError(null);

    // todo deal with multiple pages of accounts
    getAccounts()
      .then(response => {
        const items: AccountDto[] = response.data.items;
        setAccounts(items);

        const savedId = localStorage.getItem("selectedAccountId");
        let found: AccountDto | null = null;
        if (savedId) found = items.find(i => String(i.id) === String(savedId)) ?? null;
        if (!found && items.length > 0) found = items[0];
        setSelectedAccount(found);
        if (onSelect) onSelect(found);
      })
      .catch(err => {
        console.error("Error fetching accounts:", err);
        setError("Failed to load accounts");
      })
      .finally(() => setIsRefreshing(false));
  };

  const toggle = () => setOpen(s => !s);

  const selectAccount = (account: AccountDto) => {
    setSelectedAccount(account);
    localStorage.setItem("selectedAccountId", String(account.id));
    setOpen(false);

    if (onSelect) {
      onSelect(account)
    }
  };

  const getAccountTitle = (account:AccountDto) => {
    return "#" + account.id + " " + account.accountType?.title;
  };

  const renderLoading = () => {
    return <div style={{ padding: 8 }}>Loading accounts…</div>;
  };

  const renderAccountButton = (account: AccountDto) => {
    return <button
      className={selectedAccount?.id === account.id ? "selected" : ""}
      type="button"
      role="option"
      aria-selected={selectedAccount?.id === account.id}
      onClick={() => selectAccount(account)}
    >
      {getAccountTitle(account)}
    </button>;
  };

  const renderNoAccountsFound = () => {
    return <li style={{ padding: 8 }}>No accounts found</li>;
  };

  const renderError = () => {
    return <div style={{ padding: 8, color: "red" }}>Error: {error}</div>;
  }

  const renderLoadingSpinner = () => {
    return <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon>;
  };

  // Only attempt fetch after current user has been resolved.
  if (isRefreshing === null && !currentUser.isLoading && currentUser.user) {
    refresh();
  }

  const isLoading = currentUser.isLoading || isRefreshing === true;

  return (
    <div ref={rootRef}>
      <button
        type="button"
        aria-haspopup="listbox"
        aria-expanded={open}
        onClick={toggle}
        className="account-menu-button border-0"
      >
        {isLoading ? renderLoadingSpinner() : (selectedAccount ? getAccountTitle(selectedAccount) : "Select account")}
        {!isLoading && <span aria-hidden="true">▾</span>}
      </button>

      {open && (
        <div role="dialog" className="account-menu-dropdown">
          {isLoading && renderLoading()}

          {!isLoading && error && renderError()}

          {!isLoading && !error && (
            <ul role="listbox" aria-label="Accounts" className="account-menu-accounts-list">
              {accounts.length === 0 && renderNoAccountsFound()}

              {accounts.map(account => (
                <li key={account.id}>
                  {renderAccountButton(account)}
                </li>
              ))}
            </ul>
          )}
        </div>
      )}
    </div>
  );
}

export default AccountMenu;