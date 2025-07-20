using System.Diagnostics;
using MessageBrokers;
using Chess.Scripts.Messages;
using System.Text;
using Cysharp.Threading.Tasks;

namespace Chess.Scripts.BoardTasks
{
    public static class ChessEngineCommunicator
    {
        private static bool _hasReady;
        private static Process _process;
        private static string _filepath;

        private static StringBuilder _moveBuilder;

        public static void StartCommunicate()
        {
#if UNITY_EDITOR
            _filepath = "Assets/Plugins/Windows/stockfish/stockfish-windows-x86-64-avx2.exe";
#else
            _filepath = $"{Application.streamingAssetsPath}/{"stockfish-windows-x86-64-avx2.exe"}";
#endif
            _process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = _filepath,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };

            _process.StartInfo = startInfo;
            _process.OutputDataReceived += OutputReceived;
            _process.Start();
            _process.BeginErrorReadLine();
            _process.BeginOutputReadLine();

            SendMessage("uci");
            SendMessage("setoption name Threads value 8");
            SendMessage("ucinewgame");
            SendMessage("isready");

            _moveBuilder = new StringBuilder();
            _moveBuilder.Append("position startpos moves ");
        }

        public static void GetFirstMove(string move, float depth = 15)
        {
            if (_hasReady)
            {
                SendMessage($"position startpos moves {move}");
                SendMessage($"go depth {depth}");
            }
        }

        public static void GetNextMove(string move, float depth = 15)
        {
            if (_hasReady)
            {
                _moveBuilder.Append($"{move} ");
                SendMessage(_moveBuilder.ToString());
                SendMessage($"go depth {depth}");
            }
        }

        private static void SendMessage(string message)
        {
            _process.StandardInput.WriteLine(message);
            _process.StandardInput.Flush();
        }

        private static void OutputReceived(object sender, DataReceivedEventArgs e)
        {
            UniTask.SwitchToMainThread();
            string output = e.Data;

            if (string.CompareOrdinal(output, "readyok") == 0)
                _hasReady = true;

            if (output.Contains("bestmove"))
            {
                MessageBroker.Default.Publish(new BestMoveMessage
                {
                    BestMove = output
                });
            }
        }

        public static void Stop()
        {
            if (_hasReady)
                SendMessage("stop");
        }

        public static void Quit()
        {
            _moveBuilder?.Clear();

            if (_hasReady)
                SendMessage("quit");
        }
    }
}
