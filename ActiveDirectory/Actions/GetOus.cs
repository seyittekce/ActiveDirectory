using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public class GetOus
    {
        private List<PathFinder> List()
        {
            DirectoryEntry startingPoint = new DirectoryEntry(DomainActions.GetCurrentDomainPath());
            DirectorySearcher searcher = new DirectorySearcher(startingPoint)
            {
                Filter = "(objectCategory=organizationalUnit)",
                SearchScope = SearchScope.Subtree
            };
            List<PathFinder> Paths = new List<PathFinder>();
            var searcher1 = searcher.FindAll();
            foreach (SearchResult res in searcher1)
            {
                PathFinder pf = new PathFinder();
                string a = res.Path.Remove(0, 7);
                string[] b = a.Split(',');
                ArrayList c = new ArrayList();
                foreach (string item in b.Reverse())
                    if (!item.Contains("DC="))
                        if (!item.Contains("Domain Controllers"))
                            if (!item.Contains("Domain Users"))
                                if (!item.Contains("Rac"))
                                    if (!string.IsNullOrEmpty(item))
                                        c.Add(item.Remove(0, 3));
                if (c.Count != 0)
                {
                    pf.Name = string.Join("/", c.ToArray());
                    pf.Path = res.Path;
                    Paths.Add(pf);
                }

                c.Clear();
            }

            return Paths;
        }

        private List<Ou> oUs()
        {
            List<Ou> ou = new List<Ou>();
            foreach (var item in List())
            {
                string[] items = item.Name.Split('/');
                Ou ss = new Ou();
                ss.Company = items[0];
                ss.Path = item.Path;
                if (items.Count() == 2)
                    if (!string.IsNullOrEmpty(items[1]))
                        ss.City = items[1];
                ou.Add(ss);
            }

            return ou;
        }

        private bool CheckList(ArrayList ss, string Check)
        {
            return ss.Contains(Check);
        }

        public ArrayList Company()
        {
            ArrayList ssa = new ArrayList();
            var job = string.Empty;
            foreach (var item in oUs())
                if (CheckList(ssa, item.Company))
                {
                }
                else
                {
                    if (item.Company != null)
                    {
                        job = item.Company;
                        ssa.Add(item.Company);
                    }
                }

            return ssa;
        }

        public List<Ou> City(string OUs)
        {
            List<Ou> ssa = new List<Ou>();
            var SelectCity = (from ss in oUs() where ss.Company == OUs select ss).ToList();
            foreach (var item in SelectCity)
                if (!string.IsNullOrEmpty(item.City))
                    ssa.Add(item);
            return ssa;
        }
    }
}