using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto.Comment;
using BeerOn.Repo.Interfaces;
using BeerOn.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeerOn.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> IfExistComment(int commentId)
        {
           var comment = await _repository.GetAsync(commentId);
            return comment != null;
        }

        public async Task<CommentDto> GetCommentAsync(int commentId)
        {
            var comment = await _repository
                .GetAllIncluding(a => a.User)
                .SingleOrDefaultAsync(a => a.Id == commentId);
            var result = _mapper.Map<CommentDto>(comment);
            return result;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsAsync()
        {
            var comment = await _repository
                .GetAllIncluding(a => a.User)
                .OrderByDescending(d=>d.CreateDateTime)
                .ToListAsync();
            var result = _mapper.Map<IEnumerable<CommentDto>>(comment);
            return result;
        }
        public async Task<CommentDto> CreateCommentAsync(int userLogged, int beerId,SaveCommentDto commentDto)
        {
            var comment = _mapper.Map<SaveCommentDto, Comment>(commentDto);
            comment.BeerId = beerId;
            comment.UserId = userLogged;
            comment.CreateDateTime = DateTime.Now;
            await _repository.AddAsyn(comment);
            await _repository.SaveAsync();

            return await GetCommentAsync(comment.Id);
        }

        public async Task UpdateCommentAsync(int commentId, SaveCommentDto commentDto)
        {
            var comment = await _repository.GetAsync(commentId);
            commentDto.UpdateDateTime=DateTime.Now;
            _mapper.Map(commentDto, comment);
            await _repository.SaveAsync();
        }

        public async Task<bool> IfBeerExistAsync(int beerId)
        {
            return await _repository.IfExistBeer(beerId);
        }

        public async Task RemoveComment(int commentId)
        {
            var comment = await _repository.GetAsync(commentId);
            await _repository.DeleteAsyn(comment);
        }
    }
}