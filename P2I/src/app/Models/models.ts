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
  equipe: Equipe | null;
}

export interface Game {
  isStarted: boolean;
  id: number;
  name: string;
  moderators: User[];
  players: UserIG[];
  equipes: Equipe[];
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
