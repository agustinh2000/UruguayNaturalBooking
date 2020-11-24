export class TouristSpotForRequestModel {
  id: string;
  name: string;
  description: string;
  regionId: string;
  imagePath: string;
  listOfCategoriesId: string[];

  public constructor(init?: Partial<TouristSpotForRequestModel>) {
    Object.assign(this, init);
  }
}
