﻿using System;
using System.Collections.Generic;

namespace Backend_Eternal.ApplicationData;

public partial class UsersView
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Balance { get; set; }
}