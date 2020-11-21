import { Injectable } from '@angular/core';
import { Region } from '../../models/Region';


@Injectable({
  providedIn: 'root'
})

export class RegionServiceService {

  readonly regions: Region[] = [
    { Id: '13046b7e-3d83-4576-b459-65c4c965b037',
      Name: 0,
      Description: 'Región Metropolitana',
      pathOfPhoto: '../../assets/img/RegionMetropolitana.jpg'},

      {Id: 'fcceee6e-5b30-433b-bcd7-10b45af6efc5',
      Name: 1,
      Description: 'Región Sur',
      pathOfPhoto: '../../assets/img/RegionCentroSur.jpg'},

      {Id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
      Name: 2,
      Description: 'Región Este',
      pathOfPhoto: '../../assets/img/RegionEste.jpg'},

      {Id: 'cb4f3a7b-1bff-4c85-a7c9-44a79d4c2c0a',
      Name: 3,
      Description: 'Región Norte',
      pathOfPhoto: '../../assets/img/RegionNorte.jpg'},

      {Id: 'bcc6f5bc-d580-41ea-a28a-0626784cfee0',
      Name: 4,
      Description: 'Región Pajaros Pintados',
      pathOfPhoto: '../../assets/img/RegionPajarosPintadosjpg.jpg'},
  ];

  constructor() { }

  getRegions(): Region[] {
    const regionsObteined: Region[] = [];
    for (const region of this.regions){
      regionsObteined.push(region);
    }
    return regionsObteined;
  }

  isValidRegion(idRegion: string): boolean {
    const regionsObteined: Region[] = this.getRegions();
    return regionsObteined.some(r => r.Id === idRegion);
  }
}
