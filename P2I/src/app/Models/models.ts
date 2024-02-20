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
}

export interface Game {
  id: number;
  name: string;
  password: string | null;
  moderators: User[];
  players: UserIG[];
}
