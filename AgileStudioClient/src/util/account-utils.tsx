import type {AccountDto} from "../api/dtos/accounts/AccountDtos.tsx";

export const getAccountTitle = (account: AccountDto) => {
  return `#${account.id} ${account.accountType.title}`;
}