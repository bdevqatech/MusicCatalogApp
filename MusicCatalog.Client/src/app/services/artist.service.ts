import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export interface Artist {
  id: number; 
  name: string;
  bio?: string;
  country?: string;
  website?: string;
  imageUrl?: string;
}


@Injectable({
  providedIn: 'root'
})
export class ArtistService {

  private apiUrl = `${environment.apiUrl}/artist`;

  constructor(private http: HttpClient) { }

  getAllArtists(): Observable<Artist[]> {
    return this.http.get<Artist[]>(this.apiUrl);
  } 

  getArtistById(id: number): Observable<Artist> {
    return this.http.get<Artist>(`${this.apiUrl}/${id}`);
  }

  createArtist(artist: Artist): Observable<Artist> {
    return this.http.post<Artist>(this.apiUrl, artist);
  }

  updateArtist(id: number, artist: Artist): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, artist);
  }

  deleteArtist(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
