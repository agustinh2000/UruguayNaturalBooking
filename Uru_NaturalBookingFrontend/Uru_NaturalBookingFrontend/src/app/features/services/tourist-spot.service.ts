import { Injectable } from '@angular/core';
import { TouristSpotForRequestModel } from 'src/app/models/TouristSpotForRequestModel';
import { TouristSpotModelForLodgingResponseModel } from '../../models/TouristSpotModelForLodgingResponseModel';
import { TouristSpotModelForResponse } from '../../models/TouristSpotModelForResponse';
import { Region } from '../../models/Region';
import { CategoryModel } from '../../models/CategoryModel';

@Injectable({
  providedIn: 'root'
})
export class TouristSpotService {

  readonly touristSpots: TouristSpotModelForLodgingResponseModel[] = [
    {
      Id: '13046b7e-3d83-4576-b459-65c4c965b037',
      Name: 'Punta del este'
    },

    {
      Id: 'fcceee6e-5b30-433b-bcd7-10b45af6efc5',
      Name: 'San Ramón'
    },

    {
      Id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
      Name: 'Atlantida'
    },

    {
      Id: 'cb4f3a7b-1bff-4c85-a7c9-44a79d4c2c0a',
      Name: 'Las Toscas'
    },

    {
      Id: 'bcc6f5bc-d580-41ea-a28a-0626784cfee0',
      Name: 'La floresta'
    },
  ];

  readonly touristSpotsModelForResponse: TouristSpotModelForResponse[] = [
    {
      Id: '13046b7e-3d83-4576-b459-65c4c965b037',
      Name: 'Punta del este',
      Description: 'Un lugar inolvidable, donde se pasa un buen rato.',
      RegionModel: {
        Id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
        Name: 2,
        Description: 'Región Este',
        pathOfPhoto: '../../assets/img/RegionEste.jpg'
      },
      ImagePath: '../../assets/img/RegionEste.jpg',
      ListOfCategoriesModel: [{
        Id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
        Name: 'Playa'
      }]
    },

    {
      Id: 'fcceee6e-5b30-433b-bcd7-10b45af6efc5',
      Name: 'San Ramón',
      Description: 'Donde las motos te retumban la casa.',
      RegionModel: {
        Id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
        Name: 2,
        Description: 'Región Este',
        pathOfPhoto: '../../assets/img/RegionEste.jpg'
      },
      ImagePath: '../../assets/img/RegionEste.jpg',
      ListOfCategoriesModel: [{
        Id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
        Name: 'Monte'
      }]
    },

    {
      Id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
      Name: 'Atlantida',
      Description: 'Donde encontras la tienda inglesa mas grande de america',
      RegionModel: {
        Id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
        Name: 2,
        Description: 'Región Este',
        pathOfPhoto: '../../assets/img/RegionEste.jpg'
      },
      ImagePath: '../../assets/img/RegionEste.jpg',
      ListOfCategoriesModel: [{
        Id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
        Name: 'Monte'
      }, {
        Id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
        Name: 'Playa'
      }]
    },

    {
      Id: 'cb4f3a7b-1bff-4c85-a7c9-44a79d4c2c0a',
      Name: 'Las Toscas',
      Description: 'Al lado de Atlantic City',
      RegionModel: {
        Id: '13046b7e-3d83-4576-b459-65c4c965b037',
        Name: 0,
        Description: 'Región Metropolitana',
        pathOfPhoto: '../../assets/img/RegionMetropolitana.jpg'
      },
      ImagePath: '../../assets/img/RegionMetropolitana.jpg',
      ListOfCategoriesModel: [{
        Id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
        Name: 'Monte'
      }, {
        Id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
        Name: 'Playa'
      }]
    },

    {
      Id: 'bcc6f5bc-d580-41ea-a28a-0626784cfee0',
      Name: 'La floresta',
      Description: 'Cerca de atlantida',
      RegionModel: {
        Id: '13046b7e-3d83-4576-b459-65c4c965b037',
        Name: 0,
        Description: 'Región Metropolitana',
        pathOfPhoto: '../../assets/img/RegionMetropolitana.jpg'
      },
      ImagePath: '../../assets/img/RegionMetropolitana.jpg',
      ListOfCategoriesModel: [{
        Id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
        Name: 'Monte'
      }, {
        Id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
        Name: 'Playa'
      },
      {
        Id: '6714839a-ccf8-4e55-b858-476a9fe68606',
        Name: 'Atracciones'
      }, 
      {
        Id: '6714839a-ccf8-4e55-b858-476a9fe68608',
        Name: 'Putin'
      }
      ]
    },

    {
      Id: 'bcc6f5bc-d580-41ea-a28a-0626784cfee0',
      Name: 'San Gregorio del Polanco',
      Description: 'En el departamente de Tacuarembo',
      RegionModel: {
        Id: 'cb4f3a7b-1bff-4c85-a7c9-44a79d4c2c0a',
        Name: 3,
        Description: 'Región Norte',
        pathOfPhoto: '../../assets/img/RegionNorte.jpg'
      },
      ImagePath: '../../assets/img/RegionNorte.jpg',
      ListOfCategoriesModel: [{
        Id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
        Name: 'Monte'
      }, {
        Id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
        Name: 'Playa'
      },
      {
        Id: '6714839a-ccf8-4e55-b858-476a9fe68606',
        Name: 'Atracciones'
      },
      {
        Id: '6714839a-ccf8-4e55-b858-476a9fe68608',
        Name: 'Putin'
      }
      ]
    }
  ];

  constructor() { }

  getTouristSpots(): TouristSpotModelForLodgingResponseModel[] {
    const touristSpotObteined: TouristSpotModelForLodgingResponseModel[] = [];
    for (const touristSpot of this.touristSpots) {
      touristSpotObteined.push(touristSpot);
    }
    return touristSpotObteined;
  }

  getAllTouristSpots(): TouristSpotModelForResponse[] {
    const touristSpotObteined: TouristSpotModelForResponse[] = [];
    for (const touristSpot of this.touristSpotsModelForResponse) {
      touristSpotObteined.push(touristSpot);
    }
    return touristSpotObteined;
  }

  Add(touristSpotToAdd: TouristSpotForRequestModel): TouristSpotModelForResponse { return; }

  getTouristSpotsFilterByRegionAndCategories(categoriesSelected: string[], regionSelected: string): TouristSpotModelForResponse[]{
    return this.getAllTouristSpots();
  }

  getTouristSpotById(touristSpotId: string): TouristSpotForRequestModel{
    return;
    // this is a call to the webAPI
  }

}
