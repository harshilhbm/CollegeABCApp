export class Student {
    Id: string;
    StudentName: string;
    Middlename: string;
    Lastname: string;
    Phone: string;
    Gender: string;
    Address: string;
    Country: string;
    State: string;
    City: string;
    Cityid: string;
    StateId: string;
    CountryId: string;
    Zipcode: string;
}
export class Country {
    CountryId: string;
    CountryName: string;
  }
  
  export class State {
    StateId: string;
    StateName: string;
    CountryId: string;
  }
  
  export class City {
    Cityid: string;
    CityName: string;
    StateId: string;
    CountryId: string;
  }

  
export interface DialogData {
  Id: string;
}