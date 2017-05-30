using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRNGroupApp.Data;

namespace CRNGroupApp.Services
{
    public class ListService
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private readonly Guid _userID;
        public ListService(Guid userId)
        {
            _userID = userId;
        }

        public IEnumerable<ShoppingList> GetLists()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ShoppingLists
                        .Where(e => e.UserId == _userID);
                        /*.Select(
                            e =>
                                new ShoppingList
                                {
                                    ShoppingListId = e.ShoppingListId,
                                    Name = e.Name,
                                    UserId = _userID,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                    Color = e.Color,
                                    ShoppingListItems = e.ShoppingListItems
                                }
                        ).ToList();*/

                return query.ToArray();
            }
        }
    }
}
