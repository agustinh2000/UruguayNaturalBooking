import { Injectable } from '@angular/core';
import { TouristSpotForRequestModel } from 'src/app/models/TouristSpotForRequestModel';
import { TouristSpotModelForLodgingResponseModel } from '../../models/TouristSpotModelForLodgingResponseModel';
import { TouristSpotModelForResponse } from '../../models/TouristSpotModelForResponse';
import { Region } from '../../models/Region';
import { CategoryModel } from '../../models/CategoryModel';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TouristSpotService {
  uri = `${environment.baseUrl}api/touristSpots`;

  readonly touristSpots: TouristSpotModelForLodgingResponseModel[] = [
    {
      id: '13046b7e-3d83-4576-b459-65c4c965b038',
      name: 'Punta del este',
    },

    {
      id: 'fcceee6e-5b30-433b-bcd7-10b45af6efc5',
      name: 'San Ramón',
    },

    {
      id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
      name: 'Atlantida',
    },

    {
      id: 'cb4f3a7b-1bff-4c85-a7c9-44a79d4c2c0a',
      name: 'Las Toscas',
    },

    {
      id: 'bcc6f5bc-d580-41ea-a28a-0626784cfee0',
      name: 'La floresta',
    },
  ];

  readonly touristSpotsModelForResponse: TouristSpotModelForResponse[] = [
    {
      id: '13046b7e-3d83-4576-b459-65c4c965b038',
      name: 'Punta del este',
      description: 'Un lugar inolvidable, donde se pasa un buen rato.',
      regionModel: {
        id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
        name: 2,
        descriptionOfName: 'Región Este',
        pathOfPhoto: '../../assets/img/RegionEste.jpg',
      },
      imagePath: '../../assets/img/RegionEste.jpg',
      listOfCategoriesModel: [
        {
          id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
          name: 'Playa',
        },
      ],
    },

    {
      id: 'fcceee6e-5b30-433b-bcd7-10b45af6efc5',
      name: 'San Ramón',
      description: 'Donde las motos te retumban la casa.',
      regionModel: {
        id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
        name: 2,
        descriptionOfName: 'Región Este',
        pathOfPhoto: '../../assets/img/RegionEste.jpg',
      },
      imagePath: '../../assets/img/RegionEste.jpg',
      listOfCategoriesModel: [
        {
          id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
          name: 'Monte',
        },
      ],
    },

    {
      id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
      name: 'Atlantida',
      description: 'Donde encontras la tienda inglesa mas grande de america',
      regionModel: {
        id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
        name: 2,
        descriptionOfName: 'Región Este',
        pathOfPhoto: '../../assets/img/RegionEste.jpg',
      },
      imagePath: '../../assets/img/RegionEste.jpg',
      listOfCategoriesModel: [
        {
          id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
          name: 'Monte',
        },
        {
          id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
          name: 'Playa',
        },
      ],
    },

    {
      id: 'cb4f3a7b-1bff-4c85-a7c9-44a79d4c2c0a',
      name: 'Las Toscas',
      description: 'Al lado de Atlantic City',
      regionModel: {
        id: '13046b7e-3d83-4576-b459-65c4c965b037',
        name: 0,
        descriptionOfName: 'Región Metropolitana',
        pathOfPhoto: '../../assets/img/RegionMetropolitana.jpg',
      },
      imagePath: '../../assets/img/RegionMetropolitana.jpg',
      listOfCategoriesModel: [
        {
          id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
          name: 'Monte',
        },
        {
          id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
          name: 'Playa',
        },
      ],
    },

    {
      id: 'bcc6f5bc-d580-41ea-a28a-0626784cfee0',
      name: 'La floresta',
      description: 'Cerca de atlantida',
      regionModel: {
        id: '13046b7e-3d83-4576-b459-65c4c965b037',
        name: 0,
        descriptionOfName: 'Región Metropolitana',
        pathOfPhoto: '../../assets/img/RegionMetropolitana.jpg',
      },
      imagePath: '../../assets/img/RegionMetropolitana.jpg',
      listOfCategoriesModel: [
        {
          id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
          name: 'Monte',
        },
        {
          id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
          name: 'Playa',
        },
        {
          id: '6714839a-ccf8-4e55-b858-476a9fe68606',
          name: 'Atracciones',
        },
        {
          id: '6714839a-ccf8-4e55-b858-476a9fe68608',
          name: 'Putin',
        },
      ],
    },

    {
      id: 'bcc6f5bc-d580-41ea-a28a-0626784cfee0',
      name: 'San Gregorio del Polanco',
      description: 'En el departamente de Tacuarembo',
      regionModel: {
        id: 'cb4f3a7b-1bff-4c85-a7c9-44a79d4c2c0a',
        name: 3,
        descriptionOfName: 'Región Norte',
        pathOfPhoto: '../../assets/img/RegionNorte.jpg',
      },
      imagePath: '../../assets/img/RegionNorte.jpg',
      listOfCategoriesModel: [
        {
          id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
          name: 'Monte',
        },
        {
          id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
          name: 'Playa',
        },
        {
          id: '6714839a-ccf8-4e55-b858-476a9fe68606',
          name: 'Atracciones',
        },
        {
          id: '6714839a-ccf8-4e55-b858-476a9fe68608',
          name: 'Putin',
        },
      ],
    },
  ];

  constructor(private http: HttpClient) {}

  getTouristSpots(): TouristSpotModelForLodgingResponseModel[] {
    const touristSpotObteined: TouristSpotModelForLodgingResponseModel[] = [];
    for (const touristSpot of this.touristSpots) {
      touristSpotObteined.push(touristSpot);
    }
    return touristSpotObteined;
  }

  getAllTouristSpots(): Observable<TouristSpotModelForResponse> {
    return this.http.get<TouristSpotModelForResponse>(this.uri);
  }

  Add(
    touristSpotToAdd: TouristSpotForRequestModel
  ): Observable<TouristSpotModelForResponse> {
    let myHeaders = new HttpHeaders();
    if (localStorage.token !== undefined) {
      myHeaders = myHeaders.append('token', localStorage.token);
    }
    return this.http.post<TouristSpotModelForResponse>(
      this.uri,
      touristSpotToAdd,
      {
        headers: myHeaders,
      }
    );
  }

  getTouristSpotsFilterByRegionAndCategories(
    categoriesSelectedId: string[],
    regionIdSelected: string
  ): Observable<TouristSpotModelForResponse> {
    return this.http.get<TouristSpotModelForResponse>(
      `${this.uri}/byCategoriesAndRegion`,
      {
        params: {
          categoriesId: categoriesSelectedId,
          regionId: regionIdSelected,
        },
      }
    );
  }

  getTouristSpotById(
    touristSpotId: string
  ): Observable<TouristSpotModelForResponse> {
    return this.http.get<TouristSpotModelForResponse>(
      `${this.uri}/${touristSpotId}`
    );
  }
}
