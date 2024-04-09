import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExploreSongsComponent } from './explore-songs.component';

describe('ExploreSongsComponent', () => {
  let component: ExploreSongsComponent;
  let fixture: ComponentFixture<ExploreSongsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExploreSongsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExploreSongsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
