import { CategoryModel } from './CategoryModel';
import { Region } from './Region';
export class TouristSpotModelForResponse{
    public Id: string;
    public Name: string;
    public Description: string;
    public RegionModel: Region;
    public ImagePath: string;
    public ListOfCategoriesModel: CategoryModel[];
}
