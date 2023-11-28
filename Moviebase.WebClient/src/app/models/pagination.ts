export type Pagination = {
  pageNumber: number;
  pageSize: number;
  totalCount: number;
}

export class PaginatedResult<T> {
  result!: T;
  pagination!: Pagination;
}
