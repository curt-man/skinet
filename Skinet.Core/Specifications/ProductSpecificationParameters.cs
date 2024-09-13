using System;

namespace Skinet.Core.Specifications;

public class ProductSpecificationParameters
{
    private const int MAX_PAGE_SIZE = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value; }
    }



    private List<string> _brands = [];
    public List<string> Brands
    {
        get { return _brands; }
        set
        {
            _brands = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    private List<string> _types = [];
    public List<string> Types
    {
        get { return _types; }
        set
        {
            _types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    public string? Sort { get; set; }

    private string? _search;
    public string Search
    {
        get { return _search ?? ""; }
        set { _search = value.ToLower(); }
    }
    

}
