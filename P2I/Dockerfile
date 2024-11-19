FROM node:20-alpine

# Create a non-root user and group named "angular"
RUN addgroup -S angular && adduser -S angular -G angular

# Set working directory
WORKDIR /usr/src/app

# Copy files into the container
COPY . /usr/src/app

# Change ownership of the application files to the non-root user "angular"
RUN chown -R angular:angular /usr/src/app

# Install global npm packages and project dependencies
RUN npm install -g @angular/cli
RUN npm install -g serve 

# Switch to the non-root user "angular"
USER angular


RUN npm install

# Build the Angular application
RUN npm run build

# Expose port 3000
EXPOSE 3000

# Run the application
CMD ["npx", "serve", "dist/p2-i", "-p", "3000"]
