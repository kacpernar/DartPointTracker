import { cleanupOutdatedCaches, precacheAndRoute } from 'workbox-precaching'
import { clientsClaim } from 'workbox-core'
import { registerRoute } from 'workbox-routing'
import { NetworkFirst, NetworkOnly } from 'workbox-strategies'
import { BackgroundSyncPlugin } from 'workbox-background-sync'

self.skipWaiting()
clientsClaim()

cleanupOutdatedCaches()

precacheAndRoute(self.__WB_MANIFEST)

// Create a Background Sync Plugin instance
const bgSyncPlugin = new BackgroundSyncPlugin('gameQueue', {
    maxRetentionTime: 24 * 60 // Retry for up to 24 hours
})
// Runtime caching for API calls with Network First strategy
registerRoute(
    ({ url }) => url.pathname.startsWith('/Players'),
    new NetworkFirst({
        cacheName: 'players-api-cache',
        networkTimeoutSeconds: 10,
        cacheableResponse: { statuses: [0, 200] }
    })
)

// Register route to handle POST requests for games
registerRoute(
    ({ request }) => request.url.includes('/Game') && request.method === 'POST',
    new NetworkOnly({
        plugins: [bgSyncPlugin] // Apply Background Sync
    }),
    'POST'
)

self.addEventListener('sync', (event) => {
    console.log('Background sync event fired!', event)
    if (event.tag === 'gameQueue') {
        event.waitUntil(
            self.registration.showNotification('Game save completed!', {
                body: 'Your offline game has been successfully saved!',
                icon: '/favicon.svg' // Optional: add a custom icon for the notification
            })
        )
    }
})