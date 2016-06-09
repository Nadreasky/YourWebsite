using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourWebsite.Repository;

namespace YourWebsite.Services
{
    public class NewsService
    {
        NewsRepository _newsRepository;
        public NewsService()
        {
            _newsRepository = new NewsRepository();
        }

        public List<News> getAll()
        {
            return _newsRepository.List.ToList();
        }

        public void addNews(int id, string title, DateTime publishDate, string thumbnail, int popular, string content, string shortDes)
        {
            News n = findByID(id);
            if (n ==  null)
            {
                n = new News();
                n.Title = title;
                n.PublishDate = publishDate;
                n.MainImage = thumbnail;
                n.Popular = popular;
                n.Content = content;
                n.ShortDes = shortDes;
                _newsRepository.Add(n);
            }
            else
            {
                n.Title = title;
                n.PublishDate = publishDate;
                n.MainImage = thumbnail;
                n.Popular = popular;
                n.Content = content;
                n.ShortDes = shortDes;
                _newsRepository.Update(n);
            }

        }

        public void delete(News n)
        {
            _newsRepository.Delete(n);
        }

        public News findByID(int id)
        {
            if (id < 0)
            {
                return null;
            }
            News n = _newsRepository.FindById(id);
            return n;
        }

        public List<News> getRelativeNews(int id)
        {
            News mainNews = findByID(id);
            List<News> relativeNews = new List<News>();
            List<News> allNews = getAll();
            int count = 0;

            int indexOfMainNews = allNews.IndexOf(mainNews);




            for (int i = indexOfMainNews + 1; i < allNews.Count && count < 4; i++)
            {

                News p = allNews.ElementAt(i);

                relativeNews.Add(p);
                count++;
            }

            count = 0;

            for (int i = indexOfMainNews - 1; i >= 0 && count < 4; i--)
            {

                News p = allNews.ElementAt(i);
                relativeNews.Add(p);
                count++;
            }


            return relativeNews;

        }
    }
}