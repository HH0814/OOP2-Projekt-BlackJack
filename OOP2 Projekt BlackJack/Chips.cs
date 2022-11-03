// namespace Projekt 
// {
// class Chips
//  {
//      public int chipStack {get; set; }
//      public int bet {get; set; }
//      public int initializeSChipStack(){ //Ar denna ens nodvandig?
//          return chipStack = 0;
//      }
//      public int addChips(int input){
//          return chipStack += input;
//      }
     
//      public int subtractChips(int input){
//          return chipStack -= input;
//      }

//      public int emptyChipStack(){  //Ar denna ens nodvandig? Kanske for att tomma sjalva potten men borde ju hanteras med transferChips.
//          return chipStack = 0;
//      }
//      public void transferChips(Chips receivingChipStack, Chips givingChipStack, int input){
//          Participant.Player().bet.addChips(input); // man borde ha en superklass till player och dealer som heter participant.
//          receivingChipStack.addChips(input);
//          givingChipStack.subtractChips(input);
//      }
//      public string printChipStack(){
//          return chipStack.ToString();
//      }
//      public string printBet(){
//         return bet.ToString();
//      }
//      public int getBet(){
//         return bet;
//      }
//      public int getChipstack(){
//         return chipStack;
//      }
//  }
// }