export interface User {
  id: number;
  username: string;
  password: string;
  nom: string;
  prenom: string;
  games: Game[];
}

export interface UserIG {
  id: number;
  user: User;
  game: Game;
  killsList: UserIG[];
  alive: boolean;
  cible: UserIG;
  equipe: Equipe;
}

export interface Game {
  id: number;
  name: string;
  password: string | null;
  moderators: User[];
  players: UserIG[];
}

export interface Objet {
  id: number;
  name: string;
  details: string;
  debutValidite: Date;
  finValidite: Date;
  idGame: number;
}

export interface Equipe {
  id: number;
  idGame: number;
  name: string;
  couleur: string | null;
}
