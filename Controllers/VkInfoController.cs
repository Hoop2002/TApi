using Microsoft.AspNetCore.Mvc;

using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;



namespace TApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VkInfoController : ControllerBase
    {

        private readonly ILogger<VkInfoController> _logger;

        public VkInfoController(ILogger<VkInfoController> logger)
        {
            _logger = logger;
        }


        [HttpGet(Name = "GetUserPostsInfo")]
        public IEnumerable<UserDataVk> GetUserPostsInfo(int id)
        {
            int idCount = 0; 
            string[] postTxt = new string[5];
            string[]? numOfCharacters = new string[5];
            Dictionary<char, int> letters = new(); 
            int count = 0;

            var api = new VkApi();

            try
            {
                api.Authorize(new ApiAuthParams
                {
                    //Âñòàâüòå òîêåí ïðèëîæåíèÿ! (çàðåãåñòðèðîâàòü åãî ìîæíî ñî ñâîåé ñòðàíèöû âêîíòàêòå)
                    AccessToken = ""
                });
                var dataUser = api.Users.Get(new long[] { id }).FirstOrDefault();

                var getVkApiPosts = api.Wall.Get(new WallGetParams
                {
                    OwnerId = id,
                    Count = 5
                });

                foreach (var post in getVkApiPosts.WallPosts)
                {
                    postTxt[count] = post.Text;
                    count++;
                }
                count = 0;

                DataActions.LoadLogs($"Íà÷àëñÿ ïîäñ÷åò ñèìâîëîâ -- {DateTime.Now}");
                foreach (string item in postTxt)
                {
                    foreach (char c in item)
                    {
                        if (letters.ContainsKey(c))
                        {
                            letters[c]++;
                        }
                        else
                        {
                            letters[c] = 1;
                        }
                    }
                    foreach(var p in letters)
                    {
                        numOfCharacters[count] += $"{p.Key}-{p.Value}" + " | "; 
                    }
                    count++; 
                    letters.Clear();
                }
                DataActions.LoadLogs($"Çàêîí÷èëñÿ ïîäñ÷åò ñèìâîëîâ {DateTime.Now}");

                using (ConnectionDataBase db = new ConnectionDataBase())
                {
                    UserDataVkDB user = new UserDataVkDB
                    {
                        Id = idCount,
                        Vk_Id = dataUser.Id,
                        Name = $"{dataUser.FirstName} {dataUser.LastName}",
                        NumOfIncoingLetters = numOfCharacters
                    };

                    db.UserDataVkDB.Add(user);
                    db.SaveChanges();
                    idCount++;
                }

                return Enumerable.Range(0, 5).Select(index => new UserDataVk
                {
                    Id = dataUser.Id,
                    FirstName = dataUser.FirstName,
                    LastName = dataUser.LastName,
                    TextOfTheLastFivePosts = postTxt[index],
                    NumOfIncoingLetters = numOfCharacters[index],
                    RequestSuccess = true
                }).ToArray();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Enumerable.Range(0, 5).Select(index => new UserDataVk
                {
                    Id = id,
                    FirstName = null,
                    LastName = null,
                    TextOfTheLastFivePosts = null,
                    RequestSuccess = false,
                    NumOfIncoingLetters = null,
                    UserExistence = $"Account does not exist or account is banned"
                }).ToArray();
            }
        }
    }
}
