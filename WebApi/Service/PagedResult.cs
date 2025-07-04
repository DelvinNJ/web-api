﻿namespace WebApi.Service
{
    public class PagedResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public  string? Version { get; set; } = null;
        public List<T> Data { get; set; } = new();
    }
}
