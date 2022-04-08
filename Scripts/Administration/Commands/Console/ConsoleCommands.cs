using System;
using System.Linq;
using System.Threading;
using Server.Accounting;
using Server.Engines.Help;
using Server.Network;
using static System.Console;

namespace Server.Misc
{
    internal static class ServerConsole
    {
        private static readonly Func<string> _Listen = ReadLine;

        private static string _Command;

        private static Timer _PollTimer;

        private static bool _HearConsole;

        public static void Initialize()
        {
            EventSink.ServerStarted += () =>
            {
                PollCommands();

                if (_HearConsole)
                {
                    WriteLine("Now listening to the whole shard.");
                }
            };

            EventSink.Speech += args =>
            {
                if (args.Mobile == null || !_HearConsole)
                {
                    return;
                }

                try
                {
                    if (args.Mobile.Region.Name.Length > 0)
                    {
                        WriteLine(args.Mobile.Name + " (" + args.Mobile.Region.Name + "): " + args.Speech);
                    }
                    else
                    {
                        WriteLine("" + args.Mobile.Name + ": " + args.Speech + "");
                    }
                }
                catch
                { }
            };
        }

        private static void PollCommands()
        {
            _PollTimer = Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromMilliseconds(100), ProcessCommand);

            _Listen.BeginInvoke(r => ProcessInput(_Listen.EndInvoke(r)), null);
        }

        private static void ProcessInput(string input)
        {
            if (!Core.Crashed && !Core.Closing)
            {
                Interlocked.Exchange(ref _Command, input);
            }
        }

        private static void ProcessCommand()
        {
            if (Core.Crashed || Core.Closing || World.Loading || World.Saving)
            {
                return;
            }

            if (string.IsNullOrEmpty(_Command))
            {
                return;
            }

            ProcessCommand(_Command);

            Interlocked.Exchange(ref _Command, string.Empty);

            _Listen.BeginInvoke(r => ProcessInput(_Listen.EndInvoke(r)), null);
        }

        private static PageEntry[] _Pages;

        private static void ProcessCommand(string input)
        {
            input = input.Trim();

            if (_Pages != null)
            {
                HandlePaging(input);
                return;
            }

            if (input.StartsWith("pages", StringComparison.OrdinalIgnoreCase))
            {
                HandlePaging(input.Substring(5).Trim());
                return;
            }

            if (input.StartsWith("bc", StringComparison.OrdinalIgnoreCase))
            {
                var sub = input.Substring(2).Trim();

                BroadcastMessage(AccessLevel.Player, 0x35, string.Format("[Admin] {0}", sub));

                WriteLine("[World]: {0}", sub);
                return;
            }

            if (input.StartsWith("sc", StringComparison.OrdinalIgnoreCase))
            {
                var sub = input.Substring(2).Trim();

                BroadcastMessage(AccessLevel.Counselor, 0x32, string.Format("[Admin] {0}", sub));

                WriteLine("[Staff]: {0}", sub);
                return;
            }

            if (input.StartsWith("ban", StringComparison.OrdinalIgnoreCase))
            {
                var sub = input.Substring(3).Trim();

                var states = NetState.Instances;

                if (states.Count == 0)
                {
                    WriteLine("There are no players online.");
                    return;
                }

                var ns = states.Find(o => o.Account != null && o.Mobile != null && Insensitive.StartsWith(sub, o.Mobile.RawName));

                if (ns != null)
                {
                    WriteLine("[Ban]: {0}: Mobile: '{1}' Account: '{2}'", ns, ns.Mobile.RawName, ns.Account.Username);

                    ns.Dispose();
                }

                return;
            }

            if (input.StartsWith("kick", StringComparison.OrdinalIgnoreCase))
            {
                var sub = input.Substring(4).Trim();

                var states = NetState.Instances;

                if (states.Count == 0)
                {
                    WriteLine("There are no players online.");
                    return;
                }

                var ns = states.Find(o => o.Account != null && o.Mobile != null && Insensitive.StartsWith(sub, o.Mobile.RawName));

                if (ns != null)
                {
                    WriteLine("[Kick]: {0}: Mobile: '{1}' Account: '{2}'", ns, ns.Mobile.RawName, ns.Account.Username);

                    ns.Dispose();
                }

                return;
            }

            switch (input.Trim())
            {
                case "crash":
                    {
                        Timer.DelayCall(() => { throw new Exception("Forced Crash"); });
                    }
                    break;
                case "shutdown":
                    {
                        AutoSave.Save();
                        Core.Kill(false);
                    }
                    break;
                case "shutdown nosave":
                    {
                        Core.Kill(false);
                    }
                    break;
                case "restart":
                    {
                        AutoSave.Save();
                        Core.Kill(true);
                    }
                    break;
                case "restart nosave":
                    {
                        Core.Kill(true);
                    }
                    break;
                case "online":
                    {
                        var states = NetState.Instances;

                        if (states.Count == 0)
                        {
                            WriteLine("There are no users online at this time.");
                        }

                        foreach (var t in states)
                        {
                            var a = t.Account as Account;

                            if (a == null)
                            {
                                continue;
                            }

                            var m = t.Mobile;

                            if (m != null)
                            {
                                WriteLine("- Account: {0}, Name: {1}, IP: {2}", a.Username, m.Name, t);
                            }
                        }
                    }
                    break;
                case "save":
                    AutoSave.Save();
                    break;
                case "hear":
                    {
                        _HearConsole = !_HearConsole;

                        WriteLine("{0} sending speech to the console.", _HearConsole ? "Now" : "No longer");
                    }
                    break;
                default:
                    DisplayHelp();
                    break;
            }
        }

        private static void DisplayHelp()
        {
            WriteLine(" ");
            WriteLine("Commands:");
            WriteLine("crash           - Forces an exception to be thrown.");
            WriteLine("save            - Performs a forced save.");
            WriteLine("shutdown        - Performs a forced save then shuts down the server.");
            WriteLine("shutdown nosave - Shuts down the server without saving.");
            WriteLine("restart         - Sends a message to players informing them that the server is");
            WriteLine("                  restarting, performs a forced save, then shuts down and");
            WriteLine("                  restarts the server.");
            WriteLine("restart nosave  - Restarts the server without saving.");
            WriteLine("online          - Shows a list of every person online:");
            WriteLine("                  Account, Char Name, IP.");
            WriteLine("bc <message>    - Type this command and your message after it.");
            WriteLine("                  It will then be sent to all players.");
            WriteLine("sc <message>    - Type this command and your message after it.");
            WriteLine("                  It will then be sent to all staff.");
            WriteLine("hear            - Copies all local speech to this console:");
            WriteLine("                  Char Name (Region name): Speech.");
            WriteLine("ban <name>      - Kicks and bans the users account.");
            WriteLine("kick <name>     - Kicks the user.");
            WriteLine("pages           - Enter page mode to handle help requests.");
            WriteLine("help|?          - Shows this list.");
            WriteLine(" ");
        }

        private static void DisplayPagingHelp()
        {
            WriteLine(" ");
            WriteLine("Paging Commands:");
            WriteLine("view <id>              - View sender message.");
            WriteLine("remove <id>            - Remove without message.");
            WriteLine("handle <id> <message>  - Remove with message.");
            WriteLine("clear                  - Clears the page queue.");
            WriteLine("exit                   - Exit page mode.");
            WriteLine("help|?                 - Shows this list.");
            WriteLine(" ");
        }

        private static void HandlePaging(string sub)
        {
            if (sub.StartsWith("help", StringComparison.OrdinalIgnoreCase) ||
                sub.StartsWith("?", StringComparison.OrdinalIgnoreCase))
            {
                DisplayPagingHelp();

                HandlePaging(string.Empty);
                return;
            }

            if (PageQueue.List.Count == 0)
            {
                WriteLine("There are no pages in the queue.");

                if (_Pages != null)
                {
                    _Pages = null;

                    WriteLine("[Pages]: Disabled page mode.");
                }

                return;
            }

            if (string.IsNullOrWhiteSpace(sub))
            {
                if (_Pages == null)
                {
                    WriteLine("[Pages]: Enabled page mode.");

                    DisplayPagingHelp();
                }

                _Pages = PageQueue.List.Cast<PageEntry>().ToArray();

                const string format = "{0:D3}:\t{1}\t{2}";

                for (var i = 0; i < _Pages.Length; i++)
                {
                    WriteLine(format, i + 1, _Pages[i].Type, _Pages[i].Sender);
                }

                return;
            }

            if (sub.StartsWith("exit", StringComparison.OrdinalIgnoreCase))
            {
                if (_Pages != null)
                {
                    _Pages = null;

                    WriteLine("[Pages]: Disabled page mode.");
                }

                return;
            }

            if (sub.StartsWith("clear", StringComparison.OrdinalIgnoreCase))
            {
                if (_Pages != null)
                {
                    foreach (var page in _Pages)
                    {
                        PageQueue.Remove(page);
                    }

                    WriteLine("[Pages]: Queue cleared.");

                    Array.Clear(_Pages, 0, _Pages.Length);

                    _Pages = null;

                    WriteLine("[Pages]: Disabled page mode.");
                }

                return;
            }

            if (sub.StartsWith("remove", StringComparison.OrdinalIgnoreCase))
            {
                string[] args;

                var page = FindPage(sub, out args);

                if (page == null)
                {
                    WriteLine("[Pages]: Invalid page entry.");
                }
                else
                {
                    PageQueue.Remove(page);

                    WriteLine("[Pages]: Removed from queue.");
                }

                HandlePaging(string.Empty);
                return;
            }

            if (sub.StartsWith("handle", StringComparison.OrdinalIgnoreCase))
            {
                string[] args;

                var page = FindPage(sub, out args);

                if (page == null)
                {
                    WriteLine("[Pages]: Invalid page entry.");

                    HandlePaging(string.Empty);
                    return;
                }

                if (args.Length <= 0)
                {
                    WriteLine("[Pages]: Message required.");

                    HandlePaging(string.Empty);
                    return;
                }

                page.Sender.SendGump(new MessageSentGump(page.Sender, ServerList.ServerName, string.Join(" ", args)));

                WriteLine("[Pages]: Message sent.");

                PageQueue.Remove(page);

                WriteLine("[Pages]: Removed from queue.");

                HandlePaging(string.Empty);
                return;
            }

            if (sub.StartsWith("view", StringComparison.OrdinalIgnoreCase))
            {
                string[] args;

                var page = FindPage(sub, out args);

                if (page == null)
                {
                    WriteLine("[Pages]: Invalid page entry.");

                    HandlePaging(string.Empty);
                    return;
                }

                var idx = Array.IndexOf(_Pages, page) + 1;

                WriteLine("[Pages]: {0:D3}:\t{1}\t{2}", idx, page.Type, page.Sender);

                if (!string.IsNullOrWhiteSpace(page.Message))
                {
                    WriteLine("[Pages]: {0}", page.Message);
                }
                else
                {
                    WriteLine("[Pages]: No message supplied.");
                }

                HandlePaging(string.Empty);
                return;
            }

            if (_Pages != null)
            {
                string[] args;

                var page = FindPage(sub, out args);

                if (page != null)
                {
                    var idx = Array.IndexOf(_Pages, page) + 1;

                    WriteLine("[Pages]: {0:D3}:\t{1}\t{2}", idx, page.Type, page.Sender);

                    if (!string.IsNullOrWhiteSpace(page.Message))
                    {
                        WriteLine("[Pages]: {0}", page.Message);
                    }
                    else
                    {
                        WriteLine("[Pages]: No message supplied.");
                    }

                    HandlePaging(string.Empty);
                    return;
                }

                Array.Clear(_Pages, 0, _Pages.Length);

                _Pages = null;

                WriteLine("[Pages]: Disabled page mode.");
            }
        }

        private static PageEntry FindPage(string sub, out string[] args)
        {
            args = sub.Split(' ');

            if (args.Length > 1)
            {
                sub = args[1];

                if (args.Length > 2)
                {
                    args = args.Skip(2).ToArray();
                }
                else
                {
                    args = args.Skip(1).ToArray();
                }
            }

            int id;

            if (int.TryParse(sub, out id) && --id >= 0 && id < _Pages.Length)
            {
                var page = _Pages[id];

                if (PageQueue.List.Contains(page))
                {
                    return page;
                }
            }

            return null;
        }

        public static void BroadcastMessage(AccessLevel ac, int hue, string message)
        {
            World.Broadcast(hue, false, ac, message);
        }
    }
}
