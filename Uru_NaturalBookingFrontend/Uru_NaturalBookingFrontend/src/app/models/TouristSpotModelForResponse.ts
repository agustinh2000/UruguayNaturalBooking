import { CategoryModel } from './CategoryModel';
import { Region } from './Region';
export class TouristSpotModelForResponse {
  public id: string;
  public name: string;
  public description: string;
  public regionModel: Region;
  public imagePath: string;
  public listOfCategoriesModel: CategoryModel[];
}
