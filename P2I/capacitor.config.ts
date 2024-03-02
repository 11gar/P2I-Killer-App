import { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'com.killer.app',
  appName: 'KillerApp',
  webDir: 'dist',
  server: {
    androidScheme: 'https'
  }
};

export default config;
