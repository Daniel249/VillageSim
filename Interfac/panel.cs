using System;

class Panel {
    int Location_x;
    int Location_y;


    public void PrintText(string text, int current, ConsoleColor fcolor, ConsoleColor bcolor) {
        Console.SetCursorPosition(Location_x, Location_y + current);
        Console.ForegroundColor = fcolor;
        Console.BackgroundColor = bcolor;
        Console.Write(text);
        Console.ResetColor();
    }

    public void PrintText(string text, int current) {
        Console.SetCursorPosition(Location_x, Location_y + current);
        Console.Write(text);
    }

    // costructor
    public Panel(int loc_x, int loc_y) {
        Location_x = loc_x;
        Location_y = loc_y;
    }
}