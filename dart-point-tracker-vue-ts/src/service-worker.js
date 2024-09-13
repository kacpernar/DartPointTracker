// Import Workbox's precaching functions
import { precacheAndRoute } from 'workbox-precaching';

// Precache files (Workbox will inject these during the build process)
precacheAndRoute(self.__WB_MANIFEST);

// Listen for the 'install' event to handle service worker installation
self.addEventListener('install', (event) => {
  console.log('Service Worker installing...');
  // Optionally skip waiting, so the new service worker activates immediately
  event.waitUntil(self.skipWaiting());
});

// Listen for the 'activate' event to take control of all clients
self.addEventListener('activate', (event) => {
  console.log('Service Worker activating...');
  // Claim control of any open clients
  event.waitUntil(self.clients.claim());
});

// Example: A simple runtime cache for requests not handled by precache (e.g., API calls)
self.addEventListener('fetch', (event) => {
  // Here you can define custom fetch logic
  // Use Workbox strategies or custom caching strategies
  event.respondWith(
    caches.match(event.request).then((response) => {
      // If we have a matching response in the cache, return it
      return response || fetch(event.request).then((fetchResponse) => {
        // Optionally cache the response
        return fetchResponse;
      });
    }).catch(() => {
      // Optionally handle requests that fail (e.g., offline fallback)
      return caches.match('/offline.html');
    })
  );
});
