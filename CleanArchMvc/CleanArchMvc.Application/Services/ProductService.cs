﻿using AutoMapper;
using CleanArchMvc.Application.Dtos;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productQuery = new GetProductsQuery();

            if (productQuery == null)
            {
                throw new Exception($"Entity couldn't be loaded");
            }

            var result = await _mediator.Send(productQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
            {
                throw new Exception($"Entity couldn't be loaded");
            }
            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }

        //public async Task<ProductDTO> GetProductCategory(int? id)
        //{
        //    var pProductCategoryByIdQuery = new GetProductByIdQuery(id.Value);

        //    if (pProductCategoryByIdQuery == null)
        //    {
        //        throw new Exception($"Entity couldn't be loaded");
        //    }
        //    var result = await _mediator.Send(pProductCategoryByIdQuery);

        //    return _mapper.Map<ProductDTO>(result);
        //}

        public async Task Add(ProductDTO productDto)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDto);

            await _mediator.Send(productCreateCommand);
        }

        public async Task Update(ProductDTO productDto)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto);

            await _mediator.Send(productUpdateCommand);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);

            if (productRemoveCommand == null)
                throw new Exception($"Entity couldn't be loaded");

            await _mediator.Send(productRemoveCommand);
        }
    }
}