import type {AccountDto} from "../api/dtos/AccountDtos.tsx";

export const getAccountTitle = (account: AccountDto) => {
  return `#${account.id} ${account.accountType.title}`;
}