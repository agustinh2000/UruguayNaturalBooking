export class TouristSpotForRequestModel{
    Id: string;
    Name: string;
    Description: string;
    RegionId: string;
    ImagePath: string;
    ListOfCategoriesId: string[];

    public constructor(init?: Partial<TouristSpotForRequestModel>){
        Object.assign(this, init);
    }


}
