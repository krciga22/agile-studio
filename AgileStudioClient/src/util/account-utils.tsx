import type {AccountDto} from "../services/api/dtos/accounts/AccountDtos.tsx";

export const getAccountTitle = (account: AccountDto) => {
  return `#${account.id} ${account.accountType.title}`;
}