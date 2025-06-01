import { Component, OnInit } from '@angular/core';
import { Artist, ArtistService } from '../../services/artist.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-artist',
  imports: [CommonModule],
  templateUrl: './artist.component.html',
  styleUrl: './artist.component.scss'
})
export class ArtistComponent implements OnInit {

  artist?: Artist;
  loading = true;
  error?: string;

  constructor(
    private route: ActivatedRoute,
    private artistService: ArtistService
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.artistService.getArtistById(id).subscribe({
      next: (data) => {
        this.artist = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load artist data';
        console.error(err);
        this.loading = false;
      }
    });
  }
}
