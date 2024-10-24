﻿using System;

namespace Explorer.Blog.API.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
        public long UserId { get; set; }
        public BlogStatus Status { get; set; }
        public string ImageBase64 {  get; set; }
    }

    public enum BlogStatus
    {
        Draft,
        Published,
        Closed
    }
}
