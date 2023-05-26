using MidnightCommander.Components.PopUps;
using MidnightCommander.Components.PopUps.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Windows.WindowComponents.Editor
{
    class EditorTable
    {
        private string Path { get; set; }
        private string Name { get; set; }
        private List<string> Data { get; set; }

        private bool isEdited = false;

        private int top = 0;
        private int DrawMax;
        private int leftIndex = 0;

        private int cursorX = 0;
        private int cursorY = 1;

        private bool selectOn = false;
        private bool selectingOn = false;

        private int[] posFirst = new int[2];
        private int[] posLast = new int[2];
        private List<string> selectedList = new List<string>();

        private readonly string[] buttons = new string[10] { "Nápověda", "Uložit", "Označ", "Nahraď", "Kopie", "Přesun", "Hledat", "Smazat", "Hl. nabíd", "Konec", };

        public EditorTable(string path, string name)
        {

            Path = path;
            Name = name;
            Data = new List<string>();
            GetData();
        }
        //------------------------------------- Get Data
        private void GetData()
        {
            StreamReader sb = new StreamReader(this.Path);
            while (!sb.EndOfStream)
            {
                Data.Add(sb.ReadLine());
            }
            if (Data.Count == 0)
                Data.Add("");
            sb.Close();
        }
        //------------------------------------- Draw
        public void Draw()
        {
            Console.CursorVisible = false;
            DrawInfo();

            Console.SetCursorPosition(0, 1);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            this.DrawMax = Math.Min(Data.Count, Console.WindowHeight - 2);
            int neededEmpty = 0;
            if (Data.Count < Console.WindowHeight - 2)
                neededEmpty = (Console.WindowHeight - 2) - Data.Count;

            if (Data.Count < this.top + DrawMax)
            {
                top--;
                cursorY++;
            }
            if (selectOn)
            {
                if (selectingOn)
                {
                    posLast[0] = cursorX + leftIndex;
                    posLast[1] = cursorY + this.top - 1;
                }

                for (int i = top; i < DrawMax + top; i++)
                {
                    string str = Data[i];
                    if (str.Length <= leftIndex)
                        str = "";
                    else
                        str = str.Substring(leftIndex, Math.Min(str.Length - leftIndex, Console.WindowWidth));
                    Console.WriteLine(str.PadRight(Console.WindowWidth));
                }

                string selectedStrDraw = "";

                int posFirstX = posFirst[0];
                int posFirstY = posFirst[1];
                int posLastX = posLast[0];
                int posLastY = posLast[1];

                if (posFirstY > posLastY)
                {
                    posFirstX = posLast[0];
                    posLastX = posFirst[0];
                    posFirstY = posLast[1];
                    posLastY = posFirst[1];
                }

                for (int j = posFirstY > top ? posFirstY : top; j <= Math.Min(posLastY, this.DrawMax + top - 1); j++)
                {
                    if (posFirstY == posLastY)
                    {
                        if (posFirstX > posLastX)
                        {
                            posFirstX = posLast[0];
                            posLastX = posFirst[0];
                        }

                        if (Data[posFirstY] == "" || leftIndex >= posLastX || posFirstX >= leftIndex + Console.WindowWidth)
                            selectedStrDraw = "";
                        else if (posLastX > leftIndex + Console.WindowWidth)
                            selectedStrDraw = Data[posFirstY].Substring(posFirstX, Math.Min((Console.WindowWidth - (posFirstX - leftIndex)), Console.WindowWidth - 1));
                        else if (posFirstX >= leftIndex)
                            selectedStrDraw = Data[posFirstY].Substring(posFirstX, (posLastX - posFirstX));
                        else
                            selectedStrDraw = Data[posFirstY].Substring(leftIndex, Math.Min((posLastX - posFirstX) - (leftIndex - posFirstX), Console.WindowWidth - 1));

                        int selectY = 0;
                        if (posFirstY >= top)
                            selectY = posFirstY - top + 1;
                        if (posFirstX >= leftIndex + Console.WindowWidth)
                            break;
                        else if (posFirstX < leftIndex)
                            Console.SetCursorPosition(0, selectY);
                        else
                            Console.SetCursorPosition(posFirstX - leftIndex, selectY);
                    }
                    else
                    {
                        if ((leftIndex > posLastX && j == posLastY) || Data[j].Length <= leftIndex)
                            selectedStrDraw += "\r\n";
                        else if (j == posFirstY)
                        {
                            if (posFirstX - leftIndex > Console.WindowWidth)
                                selectedStrDraw += "\r\n";
                            else if (posFirstX >= leftIndex)
                                selectedStrDraw += Data[j].Substring(posFirstX, Math.Min(Console.WindowWidth - (posFirstX - leftIndex), Data[j].Length - posFirstX)) + "\r\n";
                            else
                                selectedStrDraw += Data[j].Substring(leftIndex) + "\r\n";
                        }
                        else if (j == posLastY)
                            selectedStrDraw += Data[j].Substring(leftIndex, posLastX - leftIndex);
                        else
                        {
                            if (posFirstX > leftIndex)
                            {
                                if (Data[j].Length > Console.WindowWidth)
                                    selectedStrDraw += Data[j].Substring(leftIndex, Console.WindowWidth) + "\r\n";
                                else
                                    selectedStrDraw += Data[j].Substring(leftIndex) + "\r\n";
                            }
                            else
                            {
                                if (Data[j].Length - leftIndex > Console.WindowWidth)
                                    selectedStrDraw += Data[j].Substring(leftIndex, Console.WindowWidth) + "\r\n";
                                else
                                    selectedStrDraw += Data[j].Substring(leftIndex) + "\r\n";
                            }
                        }
                        if (posFirstX - leftIndex > Console.WindowWidth)
                            Console.SetCursorPosition(0, (posFirstY >= top ? posFirstY - top + 1 : 1));
                        else
                            Console.SetCursorPosition((posFirstY >= top ? posFirstX - leftIndex <= 0 ? 0 : posFirstX - leftIndex : 0), (posFirstY >= top ? posFirstY - top + 1 : 1));
                    }
                }
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write(selectedStrDraw);
            }
            else
            {
                for (int i = top; i < DrawMax + top; i++)
                {
                    string str = Data[i];
                    if (str.Length <= leftIndex)
                        str = "";
                    else
                        str = str.Substring(leftIndex, Math.Min(str.Length - leftIndex, Console.WindowWidth));
                    Console.WriteLine(str.PadRight(Console.WindowWidth));
                }
            }

            for (int i = 0; i < neededEmpty; i++)
            {
                Console.WriteLine("".PadRight(Console.WindowWidth));
            }

            DrawButtons();
            Console.CursorVisible = true;
            Console.SetCursorPosition(cursorX, cursorY);
        }
        //------------------------------------- Draw Methods
        private void DrawInfo()
        {
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            string topInfo = GetInfo().PadRight(Console.WindowWidth);
            if (topInfo.Length > Console.WindowWidth)
                topInfo = topInfo.Substring(0, Console.WindowWidth);
            Console.Write(topInfo);
        }
        private string GetInfo()
        {
            int CharCount = 0;
            for (int i = 0; i < top + cursorY - 1; i++)
            {
                CharCount += Data[i].Length;
                CharCount++;
            }
            CharCount += cursorX + leftIndex;

            int CharCountAll = 0;
            foreach (string item in Data)
            {
                CharCountAll += item.Length;
                CharCountAll++;
            }
            CharCountAll--;

            StringBuilder sb = new StringBuilder();
            sb.Append(this.Name + "     ");

            sb.Append("[");
            sb.Append(selectOn ? "B" : "-");
            sb.Append(isEdited ? "M" : "-");
            sb.Append(false ? "placeholder" : "-");
            sb.Append(false ? "placeholder" : "-");
            sb.Append("] ");
            sb.Append((cursorX + leftIndex).ToString());
            sb.Append(" L:[ ");
            sb.Append((top + 1).ToString() + "+ ");
            sb.Append((cursorY - 1).ToString());
            sb.Append("  " + (top + cursorY).ToString());
            sb.Append('/' + Data.Count.ToString());
            sb.Append("] *(");
            sb.Append(CharCount.ToString());
            sb.Append(" / ");
            sb.Append(CharCountAll.ToString() + ") ");
            try
            {
                sb.Append((Convert.ToInt32(Data[top + cursorY - 1][cursorX]).ToString()).PadLeft(4, '0'));
                sb.Append(" 0x" + (Convert.ToInt32(Data[top + cursorY - 1][cursorX]).ToString("x")).PadLeft(3, '0'));
            }
            catch (Exception)
            {
                sb.Append("0010 ");
                sb.Append("0x00A");
            }
            return sb.ToString();
        }
        private void DrawButtons()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            int padding = (Console.WindowWidth / 10) - 2;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (Console.CursorLeft == Console.WindowWidth)
                    return;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write((i + 1).ToString().PadLeft(2, '0'));
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(buttons[i].PadRight(padding));
            }
        }
        //------------------------------------- Select Methods
        private void StartSelecting()
        {
            selectedList = new List<string>();

            posFirst[0] = cursorX + leftIndex;
            posFirst[1] = cursorY + this.top - 1;
            posLast[0] = cursorX + leftIndex;
            posLast[1] = cursorY + this.top - 1;

            selectingOn = true;
            selectOn = true;
        }
        private void SelectToString()
        {
            int posFirstX = posFirst[0];
            int posFirstY = posFirst[1];
            int posLastX = posLast[0];
            int posLastY = posLast[1];
            if (posFirstY > posLastY)
            {
                //if (posFirstX > posLastX)
                //{
                //    posFirstX = posLast[0];
                //    posLastX = posFirst[0];
                //}
                posFirstX = posLast[0];
                posLastX = posFirst[0];
                posFirstY = posLast[1];
                posLastY = posFirst[1];
            }
            if (posFirstY == posLastY && posFirstX > posLastX)
            {
                posFirstX = posLast[0];
                posFirstY = posLast[1];
            }

            if (posFirstY == posLastY && posFirstX != posLastX)
                selectedList.Add(Data[posFirstY].Substring(posFirstX, (posLastX - posFirstX)));
            else
            {
                for (int j = posFirstY > top ? posFirstY : top; j <= Math.Min(posLastY, this.DrawMax + top); j++)
                {
                    if (j == posFirstY)
                        selectedList.Add(Data[j].Substring(posFirstX));
                    else if (j == posLastY)
                        selectedList.Add(Data[j].Substring(0, posLastX));
                    else
                    {
                        try
                        {
                            selectedList.Add(Data[j]);

                        }
                        catch
                        {
                            selectedList.Add("");
                        }
                    }
                }
            }
        }
        //------------------------------------- Actions
        private void ExitPopUp_ExitAction(int obj)
        {
            if (obj == 0)
            {
                SaveAction(true);
                EditWindow.popUpWindow = null;
                Application.window = EditWindow.LastWindow;

                Application.Initialize();
            }
            else if (obj == 1)
            {
                EditWindow.popUpWindow = null;
                Application.window = EditWindow.LastWindow;

                Application.Initialize();
            }
            else
                EditWindow.popUpWindow = null;
        }
        private void SaveAction(bool obj)
        {
            if (obj)
            {
                isEdited = false;
                StreamWriter sw = new StreamWriter(Path);
                foreach (string item in this.Data)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
                EditWindow.popUpWindow = null;
                Application.Initialize();
            }
            else
                EditWindow.popUpWindow = null;
        }
        private void FindAction(string str)
        {
            for (int i = cursorY + top - 1; i < Data.Count; i++)
            {
                string dataPart = this.Data[i];
                if (i == cursorY + top - 1)
                    dataPart = dataPart.Substring(cursorX + leftIndex);

                if (dataPart.Contains(str))
                {
                    selectOn = true;
                    int index = this.Data[i].IndexOf(str);
                    posFirst[0] = index;
                    posFirst[1] = i;
                    posLast[0] = index + str.Length;
                    posLast[1] = i;

                    top = i > top + DrawMax ? i - DrawMax + 1 : top;
                    leftIndex = index > leftIndex + Console.WindowWidth ? index - 1 : leftIndex;
                    cursorX = index - leftIndex;
                    cursorY = i - top + 1;
                    return;
                }
            }
            Universal uni = new Universal("Výraz nenalezen", ConsoleColor.DarkRed, ConsoleColor.White, false);
            uni.Draw();
            System.Threading.Thread.Sleep(2000);
            Application.Draw();
        }
        //------------------------------------- Actions Replace
        private void ReplaceFind(string findStr, string replaceStr)
        {
            for (int i = cursorY + top - 1; i < Data.Count; i++)
            {
                string dataPart = this.Data[i];
                if (i == cursorY + top - 1)
                    dataPart = dataPart.Substring(cursorX + leftIndex);

                if (dataPart.Contains(findStr))
                {
                    selectOn = true;
                    int index = this.Data[i].IndexOf(findStr);
                    posFirst[0] = index;
                    posFirst[1] = i;
                    posLast[0] = index + findStr.Length;
                    posLast[1] = i;

                    top = i > top + DrawMax ? i - DrawMax + 1 : top;
                    leftIndex = index > leftIndex + Console.WindowWidth ? index - 1 : leftIndex;
                    cursorX = index - leftIndex;
                    cursorY = i - top + 1;
                    ReplaceActionPopUp replaceActionPopUp = new ReplaceActionPopUp(findStr, replaceStr);
                    replaceActionPopUp.ReplaceAction += ReplaceAction;
                    EditWindow.popUpWindow = replaceActionPopUp;
                    Application.Initialize();
                    return;
                }
                else
                    EditWindow.popUpWindow = null;
            }
        }
        private void ReplaceAction(int value, string find, string replace)
        {
            switch (value)
            {
                case ((int)ReplaceValue.REPLACE):
                    for (int i = cursorY + top - 1; i < Data.Count; i++)
                    {
                        string dataPart = this.Data[i];
                        if (i == cursorY + top - 1)
                            dataPart = dataPart.Substring(cursorX + leftIndex);

                        if (dataPart.Contains(find))
                        {
                            int index = this.Data[i].IndexOf(find);
                            Data[i] = Data[i].Replace(find, replace);
                            selectOn = true;
                            posFirst[0] = index;
                            posFirst[1] = i;
                            posLast[0] = index + find.Length;
                            posLast[1] = i;

                            top = i > top + DrawMax ? i - DrawMax + 1 : top;
                            leftIndex = index > leftIndex + Console.WindowWidth ? index - 1 : leftIndex;
                            cursorX = index - leftIndex;
                            cursorY = i - top + 1;
                            ReplaceFind(find, replace);
                            return;
                        }
                    }
                    break;
                case ((int)ReplaceValue.ALL):
                    cursorX = 0;
                    cursorY = 1;
                    top = 0;
                    leftIndex = 0;
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (Data[i].Contains(find))
                        {
                            Data[i] = Data[i].Replace(find, replace);
                            selectOn = false;
                            selectingOn = false;
                            isEdited = true;
                        }
                    }
                    EditWindow.popUpWindow = null;
                    break;
                case ((int)ReplaceValue.SKIP):
                    cursorX++;
                    ReplaceFind(find, replace);
                    break;
            }
        }
        //------------------------------------- HandleKey
        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.F2:
                    SaveHandle();
                    break;
                case ConsoleKey.F3:
                    SelectHandle();
                    break;
                case ConsoleKey.F4:
                    ReplaceHandle();
                    break;
                case ConsoleKey.F5:
                    if (selectOn)
                        CopyHandle();
                    break;
                case ConsoleKey.F6:
                    MoveHandle();
                    break;
                case ConsoleKey.F7:
                    FindHandle();
                    break;
                case ConsoleKey.F8:
                    RemoveHandle();
                    break;
                case ConsoleKey.F10:
                    ExitHandle();
                    break;
                //-------------------------------
                case ConsoleKey.UpArrow:
                    UpArrowHandle();
                    break;
                case ConsoleKey.DownArrow:
                    DownArrowHandle();
                    break;
                case ConsoleKey.RightArrow:
                    RightArrowHandle();
                    break;
                case ConsoleKey.LeftArrow:
                    LeftArrowHandle();
                    break;
                //-------------------------------
                case ConsoleKey.Backspace:
                    BackspaceHandle();
                    break;
                case ConsoleKey.Delete:
                    DeleteHandle();
                    break;
                case ConsoleKey.Enter:
                    EnterHandle();
                    break;
                default:
                    DefaultHandle(info);
                    break;
            }
        }
        //------------------------------------- Keys methods
        private void SaveHandle()
        {
            if (isEdited)
            {
                SavePopUp popUp = new SavePopUp();
                popUp.SaveAction += SaveAction;
                EditWindow.popUpWindow = popUp;
            }
        }
        private void SelectHandle()
        {
            if (!selectOn)
            {
                StartSelecting();
            }
            else
            {
                if (selectingOn && posFirst[0] == posLast[0] && posFirst[1] == posLast[1])
                {
                    selectingOn = false;
                    selectOn = false;
                }
                else if (selectingOn)
                {
                    SelectToString();
                    selectingOn = false;
                }
                else
                    StartSelecting();
            }
        }
        private void ReplaceHandle()
        {
            ReplaceMenuPopUp replaceMenuPopUp = new ReplaceMenuPopUp();
            replaceMenuPopUp.FindAction += ReplaceFind;
            EditWindow.popUpWindow = replaceMenuPopUp;
        }
        private void CopyHandle()
        {
            if (selectedList.Count == 1)
            {
                isEdited = true;
                this.Data[cursorY + top - 1] = this.Data[cursorY + top - 1].Insert(cursorX + leftIndex, selectedList[0]);
            }
            else if (selectedList.Count > 1)
            {
                string tempStr = this.Data[cursorY + top - 1].Substring(cursorX + leftIndex);
                if (this.Data[cursorY + top - 1] != "" && this.Data[cursorY + top - 1].Length != cursorX + leftIndex)
                    this.Data[cursorY + top - 1] = this.Data[cursorY + top - 1].Remove(cursorX + leftIndex);

                this.Data[cursorY + top - 1] = this.Data[cursorY + top - 1].Insert(cursorX + leftIndex, selectedList[0]);
                for (int i = 0; i < selectedList.Count; i++)
                {
                    if (i == selectedList.Count - 1)
                        this.Data[cursorY + top + i - 1] += tempStr;
                    else
                        this.Data.Insert(cursorY + top + i, selectedList[i + 1]);

                }
                isEdited = true;
            }
        }
        private void MoveHandle()
        {
            if (selectOn)
            {

                if (cursorY + top < posFirst[1])
                {
                    posFirst[1] += selectedList.Count - 1;
                    posLast[1] += selectedList.Count - 1;
                }

                CopyHandle();

                RemoveHandle();
            }
            selectOn = true;
            posFirst[0] = cursorX + leftIndex;
            posFirst[1] = cursorY + this.top - 1;
            if (selectedList.Count == 1)
            {
                posLast[0] = cursorX + leftIndex + this.selectedList[selectedList.Count - 1].Length;
                posLast[1] = cursorY + this.top - 1;
            }
            else
            {
                posLast[0] = this.selectedList[selectedList.Count - 1].Length;
                posLast[1] = cursorY + this.top - 2 + this.selectedList.Count;

            }
            isEdited = true;
        }
        private void FindHandle()
        {
            FindPopUp findPopUp = new FindPopUp();
            findPopUp.FindAction += FindAction;
            EditWindow.popUpWindow = findPopUp;
        }
        private void RemoveHandle()
        {
            if (selectOn)
            {
                int posFirstX = posFirst[0];
                int posFirstY = posFirst[1];
                int posLastX = posLast[0];
                //int posLastY = posLast[1];
                int DeletedRows = 0;
                if (posFirst[1] == posLast[1] && posFirst[0] != posLast[0])
                {
                    this.Data[posFirstY] = this.Data[posFirstY].Remove(posFirstX, posLastX - posFirstX);
                }
                else if (posFirst[1] != posLast[1])
                {
                    for (int i = posFirst[1]; i <= posLast[1]; i++)
                    {
                        if (i == posFirst[1])
                        {
                            this.Data[i] = this.Data[i].Remove(posFirstX);
                        }
                        else if (i - DeletedRows == posLast[1])
                        {
                            this.Data[i] = this.Data[i].Remove(0, posLastX);
                        }
                        else
                        {
                            this.Data.RemoveAt(i);
                            DeletedRows++;
                        }
                    }
                }

                selectOn = false;
                selectingOn = false;
            }
            else
            {
                this.Data.RemoveAt(cursorY + top - 1);
            }
            isEdited = true;
        }
        private void ExitHandle()
        {
            if (isEdited)
            {
                ExitPopUp exitPopUp = new ExitPopUp();
                exitPopUp.ExitAction += ExitPopUp_ExitAction;
                EditWindow.popUpWindow = exitPopUp;
            }
            else
            {
                Application.windowType = WindowType.Browser;
                Application.window = EditWindow.LastWindow;
                Application.Initialize();
            }
        }
        private void UpArrowHandle()
        {
            if (cursorY == 1 && top == 0)
                return;
            if (leftIndex != 0)
            {
                if (Data[top + cursorY - 2].Length == 0)
                {
                    leftIndex = 0;
                    cursorX = 0;
                }
                else
                {
                    leftIndex = ((Data[top + cursorY - 2].Length - Console.WindowWidth) >= 0) ? Data[top + cursorY - 2].Length - Console.WindowWidth + 1 : 0;
                    cursorX = Data[top + cursorY - 2].Length - leftIndex;
                }
            }
            else if (cursorX + leftIndex > Data[top + cursorY - 2].Length)
                cursorX = Data[top + cursorY - 2].Length;
            if (cursorY == 1)
            {
                top--;
                return;
            }
            else
                this.cursorY--;
        }
        private void DownArrowHandle()
        {
            if (top + DrawMax == Data.Count && cursorY == this.DrawMax)
                return;
            if (leftIndex != 0)
            {
                if (Data[top + cursorY].Length == 0)
                {
                    leftIndex = 0;
                    cursorX = 0;
                }
                else
                {
                    leftIndex = ((Data[top + cursorY].Length - Console.WindowWidth) >= 0) ? Data[top + cursorY].Length - Console.WindowWidth + 1 : 0;
                    cursorX = Data[top + cursorY].Length - leftIndex;
                }
            }
            else if (cursorX > Data[top + cursorY].Length)
                cursorX = Data[top + cursorY].Length;
            if (cursorY == this.DrawMax)
            {
                top++;
                return;
            }
            else
                this.cursorY++;
        }
        private void LeftArrowHandle()
        {
            if (cursorX + leftIndex == 0 && top == 0 && cursorY == 1)
                return;
            else if (cursorX == 0 && leftIndex != 0)
                leftIndex--;
            else if (cursorX == 0)
            {
                if (cursorY == 1)
                    this.top--;
                else
                    cursorY--;
                leftIndex = Data[top + cursorY - 1].Length - Math.Min(Data[top + cursorY - 1].Length, Console.WindowWidth - 1);
                cursorX = Math.Min(Data[top + cursorY - 1].Length, Console.WindowWidth - 1);
                return;
            }
            else
                this.cursorX--;
        }
        private void RightArrowHandle()
        {
            if (cursorX + leftIndex == this.Data[top + cursorY - 1].Length && cursorY == this.DrawMax && top + DrawMax == Data.Count)
                return;
            else if (cursorX + leftIndex != this.Data[top + cursorY - 1].Length && cursorX == Console.WindowWidth - 1)
                leftIndex++;
            else if (cursorX + leftIndex == this.Data[top + cursorY - 1].Length)
            {
                if (cursorY == this.DrawMax)
                {
                    this.top++;
                    leftIndex = 0;
                }
                else
                {
                    leftIndex = 0;
                    this.cursorY++;
                }
                this.cursorX = 0;
                return;
            }
            else
                this.cursorX++;
        }
        private void EnterHandle()
        {
            isEdited = true;
            if (cursorX + leftIndex == this.Data[top + cursorY - 1].Length)
            {
                this.Data.Insert(top + cursorY, "");
            }
            else
            {
                string tempRow = this.Data[top + cursorY - 1].Substring(cursorX + leftIndex);
                this.Data[top + cursorY - 1] = this.Data[top + cursorY - 1].Remove(cursorX + leftIndex);

                this.Data.Insert(top + cursorY, tempRow);
            }

            if (cursorY == DrawMax)
                top++;
            else
                cursorY++;
            leftIndex = 0;
            cursorX = 0;
        }
        private void BackspaceHandle()
        {
            if (cursorX + leftIndex == 0 && top == 0 && cursorY == 1)
                return;
            else if (this.Data[top + cursorY - 1].Length != 0 && cursorX != 0)
            {
                this.Data[top + cursorY - 1] = this.Data[top + cursorY - 1].Remove(cursorX - 1 + leftIndex, 1);
                if (cursorX == 0 && leftIndex != 0)
                {
                    leftIndex--;
                }
                else
                    cursorX--;
            }
            else if (cursorX + leftIndex == 0)
            {
                int cursorTemp = this.Data[top + cursorY - 2].Length;
                this.Data[top + cursorY - 2] += this.Data[top + cursorY - 1];
                this.Data.RemoveAt(top + cursorY - 1);
                this.cursorX = cursorTemp;
                cursorY--;
            }
            isEdited = true;
        }
        private void DeleteHandle()
        {
            if (cursorY + top == this.Data.Count && cursorX + leftIndex == this.Data[top + cursorY - 1].Length)
                return;
            else if (cursorX + leftIndex != this.Data[top + cursorY - 1].Length)
                this.Data[top + cursorY - 1] = this.Data[top + cursorY - 1].Remove(cursorX + leftIndex, 1);
            else
            {
                this.Data[top + cursorY - 1] += this.Data[top + cursorY];
                this.Data.RemoveAt(top + cursorY);
            }
            isEdited = true;
        }
        private void DefaultHandle(ConsoleKeyInfo info)
        {
            if (Char.GetUnicodeCategory(info.KeyChar) != System.Globalization.UnicodeCategory.Control)
            {
                this.Data[top + cursorY - 1] = this.Data[top + cursorY - 1].Insert(cursorX + leftIndex, info.KeyChar.ToString());
                if (cursorX == Console.WindowWidth - 1)
                    leftIndex++;
                else
                    cursorX++;
                isEdited = true;
            }
        }
    }
}
