using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos;

public class BundleDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Price { get; set; }

    public BundleStatus Status { get;  set; }

    public List<long> TourIds { get; set; }
    public long AuthorId { get; set; }

    public enum BundleStatus
    {
        DRAFT,
        PUBLISHED,
        ARCHIVED
    }

}
