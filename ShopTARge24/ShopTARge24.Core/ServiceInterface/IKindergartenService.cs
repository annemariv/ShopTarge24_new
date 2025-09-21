﻿using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;


namespace ShopTARge24.Core.ServiceInterface
{
    public interface IKindergartenService
    {
        Task<Kindergartens> Create(KindergartenDto dto);
        Task<Kindergartens> Update(KindergartenDto dto);
        Task<Kindergartens> DetailAsync(int id);
        Task<bool> Delete(int id);
    }
}
