using System;
using System.Collections.Generic;

namespace Backend_Eternal.ApplicationData;

public partial class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TagsNft> TagsNfts { get; set; } = new List<TagsNft>();
}
