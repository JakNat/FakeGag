﻿using NET_student_project.Models;
using NET_student_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NET_student_project.DataAccessLayer
{
    public class MemeRepository
    {
        private readonly GagDbContext _gagDb = new GagDbContext();
      
        public List<MemeViewModel> GetAllHotMemes()
        {
            return _gagDb.Categories.SelectMany(c => c.Memes).Where(m => m.Points > 400).ToList().Select(m => new MemeViewModel
            {
                ImagePath = m.ImagePath,
                MemeId = m.MemeId,
                Points = m.Points,
                Title = m.Title,
                SComments = m.Comments.Count
                
            }
            ).ToList();
            
        }
        public List<MemeViewModel> GetAllTrendingMemes()
        {
            return _gagDb.Categories.SelectMany(c => c.Memes).Where(m => m.Points <= 400 && m.Points > 150).Select(m => new MemeViewModel
            {
                ImagePath = m.ImagePath,
                MemeId = m.MemeId,
                Points = m.Points,
                Title = m.Title,
                SComments = m.Comments.Count

            }
            ).ToList();

        }
        public List<MemeViewModel> GetAllFreshMemes()
        {
            return _gagDb.Categories.SelectMany(c => c.Memes).Where(m => m.Points <= 150).Select(m => new MemeViewModel
            {
                ImagePath = m.ImagePath,
                MemeId = m.MemeId,
                Points = m.Points,
                Title = m.Title,
                SComments = m.Comments.Count

            }
            ).ToList();

        }
        public MemeViewModel GetMemeById(int id)
        {
            var m = _gagDb.Categories.SelectMany(c => c.Memes).First(c => c.MemeId == id);
            return new MemeViewModel
            {
                ImagePath = m.ImagePath,
                MemeId = m.MemeId,
                Points = m.Points,
                Title = m.Title,
                SComments = m.Comments.Count
            };
        }
        public List<MemeViewModel> GetMemeByCategory(string categoryName)
        {
            var a = _gagDb.Categories.Where(c => c.Name == categoryName).SelectMany(c => c.Memes).ToList();
            return a.Select(m => new MemeViewModel
            {
                ImagePath = m.ImagePath,
                MemeId = m.MemeId,
                Points = m.Points,
                Title = m.Title,
                SComments = m.Comments.Count
            }).ToList();
        }
    }
}