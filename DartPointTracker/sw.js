// Import Workbox from the CDN
importScripts('https://storage.googleapis.com/workbox-cdn/releases/7.1.0/workbox-sw.js');

if (workbox) {
    // Force the waiting service worker to become the active service worker
    self.skipWaiting();
    workbox.core.clientsClaim();

    // Clean up old caches
    workbox.precaching.cleanupOutdatedCaches();

    // Precache assets
    workbox.precaching.precacheAndRoute(self.__WB_MANIFEST);

    // Create a Background Sync Plugin instance
    const bgSyncPlugin = new workbox.backgroundSync.BackgroundSyncPlugin('gameQueue', {
        maxRetentionTime: 24 * 60 // Retry for up to 24 hours
    });

    // Runtime caching for API calls with Network First strategy
    workbox.routing.registerRoute(
        ({ url }) => url.pathname.startsWith('/Players'),
        new workbox.strategies.NetworkFirst({
            cacheName: 'players-api-cache',
            networkTimeoutSeconds: 10,
            cacheableResponse: { statuses: [200] },
        }),
        'GET'
    );

    // Register route to handle POST requests for games
    workbox.routing.registerRoute(
        ({ request }) => request.url.includes('/Game') && request.method === 'POST',
        new workbox.strategies.NetworkOnly({
            plugins: [bgSyncPlugin], // Apply Background Sync
        }),
        'POST'
    );

    // Listen for sync events to show a notification after background sync completes
    self.addEventListener('sync', (event) => {
        console.log('Background sync event fired!', event);
        if (event.tag === 'gameQueue') {
            event.waitUntil(
                self.registration.showNotification('Game save completed!', {
                    body: 'Your offline game has been successfully saved!',
                    icon: '/favicon.svg', // Optional: add a custom icon for the notification
                })
            );
        }
    });
} else {
    console.log("Workbox didn't load");
}
