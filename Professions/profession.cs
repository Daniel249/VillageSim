using System;

enum Profession {
    Farmer = 0,
    Lumberjack = 1,
    Miner = 2,
    Blacksmith = 3
}
static class ProfessionMethods {
    // cast Profession to a newly initialized Person
    public static Person ToPerson(this Profession prof) {
        Person p = null;

        switch(prof) {
            case Profession.Farmer:
                p = new Farmer();
                break;
            case Profession.Lumberjack:
                p = new Lumberjack();
                break;
            case Profession.Miner:
                p = new Miner();
                break;
            case Profession.Blacksmith:
                p = new Blacksmith();
                break;
            default:
                throw new NotSupportedException();
        }

        return p;
    }

    // cast Person to Profession
    public static Profession ToProfession(this Person person) {
        Profession profession = 0;
        
        if(person is Farmer) {
            profession = Profession.Farmer;
        } else if(person is Lumberjack) {
            profession = Profession.Lumberjack;
        } else if(person is Miner) {
            profession = Profession.Miner;
        } else if(person is Blacksmith) {
            profession = Profession.Blacksmith;
        } else {
            throw new NotSupportedException();
        }

        return profession;
    }
}