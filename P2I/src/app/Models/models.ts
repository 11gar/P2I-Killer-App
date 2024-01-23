export interface User {
  id: number;
  username: string;
  password: string;
  games: Game[];
}

export interface UserIG {
  id: number;
  user: User;
  game: Game;
  KillsList: UserIG[];
  Alive: boolean;
  Cible: UserIG;
}

export interface Game {
  id: number;
  name: string;
  password: string | null;
  moderators: User[];
  players: UserIG[];
}
