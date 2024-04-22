using LinqToDatabase.Data;
using LinqToDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinqToDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly GameDbContext _ctx;

        public PlayerController(GameDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //List<Player> result = (from p in _ctx.Players
            //                      select p).ToList();

            IQueryable<Player> result = (from p in _ctx.Players
                                       select p);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = (from p in _ctx.Players
                         where p.PlayerId == id
                         select p).SingleOrDefault();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDetails")]
        public IActionResult Get(List<int> ids)
        {
            var result = (from p in _ctx.Players
                          join id in ids
                          on p.PlayerId equals id
                          select p).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/GetFullInventory")]
        public IActionResult GetFullInventory(int id)
        {
            //tutti gli item di tutti i character di un player
            //result => Item Name, tot qnt

            //var result = (from item in _ctx.Items
            //              join inv in _ctx.Inventory on item.ItemId equals inv.InventoryId
            //              join cha in _ctx.Characters on inv.InventoryId equals cha.CharacterId
            //              where cha.PlayerId == id
            //              group inv.ItemCount by item into grouped
            //              select new { grouped.Key.Name, Total = grouped.Sum() });

            var result = (from inv in _ctx.Inventories
                          where inv.Character.PlayerId == id
                          group inv.ItemCount by inv.Item into grouped
                          select new { grouped.Key.Name, Total = grouped.Sum() });

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/InventoryDiversity")]
        public IActionResult GeAvgtInventoryDiversity(int id)
        {
            var result = (from cha in (from inv in _ctx.Inventories
                          where inv.Character.PlayerId == id
                          select new { inv.CharacterId, inv.ItemId }).Distinct()
                          group cha by cha.CharacterId into g
                          select g.Count()).Average();

            var result2 = _ctx.Inventories
                .Where(x => x.Character.PlayerId == id)
                .DistinctBy(inv => new { inv.CharacterId, inv.ItemId })
                .GroupBy(x => x.CharacterId)
                .Average(g => g.Count());

            return Ok(result);
        }

        [HttpGet]
        [Route("Ranking")]
        public IActionResult GetRanking()
        {
            var result = (from p in (from p in _ctx.Players
                            select new
                            {
                                p.PlayerId,
                                p.AccountName,
                                p.AccountLevel,
                                MaxChar = p.Characters
                                        .OrderByDescending(c => c.CharacterLevel)
                                        .FirstOrDefault()
                            })
                          orderby
                           p.AccountLevel descending,
                           p.MaxChar.CharacterLevel descending,
                           p.AccountName descending,
                           p.MaxChar.NickName descending
                          select new RankingModel
                          {
                              PlayerId = p.PlayerId,
                              PlayerName = p.AccountName,
                              PlayerLevel = p.AccountLevel,
                              CharacterId = p.MaxChar.CharacterId,
                              CharacterLevel = p.MaxChar.CharacterLevel,
                              CharacterName = p.MaxChar.NickName
                          }).Take(100).ToList();

            int pos = 1;
            result[0].Ranking = pos;
            for(int i = 1; i < result.Count; i++)
            {
                if (result[i].PlayerLevel != result[i-1].PlayerLevel || 
                    result[i].CharacterLevel != result[i - 1].CharacterLevel)
                {
                    pos = i + 1;
                }
                result[i].Ranking = pos;
            }

            return Ok(result);
        }
    }
}
