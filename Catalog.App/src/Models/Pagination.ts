export class Pagination {
  SortBy = '';
  PageSize = 10;
  PageNumber = 1;
  SortDescending = false;

  constructor(
    sortBy: string,
    pageSize: number,
    pageNumber: number,
    byDescending: boolean
  ) {
    (this.PageNumber = pageNumber),
      (this.PageSize = pageSize),
      (this.SortBy = sortBy),
      (this.SortDescending = byDescending);
  }
}
