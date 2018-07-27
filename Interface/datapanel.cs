using System;
using System.Collections.Generic;

class DataPanels {

    // panels
    List<Panel> panels;
    public TimeSpan[] logs { get; private set; }

    static int panelSize = 15;
    static int stringSize = 10;
    
    public void UpdatePrint(TimeSpan[] timespan) {
        logs = timespan;
        for(int i = 0; i < timespan.Length; i++) {
            TimeSpan current = timespan[i];
            // copy timespans to logs
            logs[i].CopyLog(current);
            // print data
            panels[i].PrintText(((Profession)i).ToString(), (int)Stats.Class);
            panels[i].PrintText(normalizeString(current.Open.ToString("#.##")), (int)Stats.Open);
            panels[i].PrintText(normalizeString(current.Close.ToString("#.##")), (int)Stats.Close);
            panels[i].PrintText(normalizeString(current.High.ToString("#.##")), (int)Stats.High);
            panels[i].PrintText(normalizeString(current.Low.ToString("#.##")), (int)Stats.Low);
            panels[i].PrintText(normalizeString(current.ResourceVolume.ToString()), (int)Stats.ResourceVol);
            panels[i].PrintText(normalizeString(current.CurrencyVolume.ToString("#.##")), (int)Stats.CurrencyVol);
            decimal averagePrice;
            try {
                averagePrice = current.CurrencyVolume / current.ResourceVolume;
            } catch {
                averagePrice = 0m;
            }
            panels[i].PrintText(normalizeString(averagePrice.ToString("#.##")), (int)Stats.AveragePrice);
        }
    }

    string normalizeString(string msg) {
        int rest = stringSize - msg.Length;
        return msg + new String(' ', rest);
    }

    
    // factory 
    public void InitPanels(int loc_x, int loc_y, int panelNum) {
        
        // int panelNum = Simulation.SimInstance.profAmmount;

        panels = new List<Panel>();
        for(int i = 0; i < panelNum; i++) {
            Panel datap = new Panel(loc_x, loc_y);
            panels.Add(datap);
            loc_x += panelSize;
        }
    }

    // constructor
    public DataPanels(int loc_x, int loc_y, int panelNum) {
        InitPanels(loc_x, loc_y, panelNum);
        logs = new TimeSpan[panelNum];
    }
}