type RequiredEnvVar = 'VITE_API_BASE_URL';

function getRequiredEnv(name: RequiredEnvVar): string {
    const value = import.meta.env[name];

    if (!value || value.trim() === '') {
        throw new Error(
            `[env] Missing required variable: ${name}. ` +
            `Set it in your .env file (see .env.example).`
        );
    }

    return value.trim();
}

function normalizeBaseUrl(value: string): string {
    let url: URL;

    try {
        url = new URL(value);
    } catch {
        throw new Error(
            `[env] VITE_API_BASE_URL must be a valid absolute URL. Received: "${value}".`
        );
    }

    if (!/^https?:$/.test(url.protocol)) {
        throw new Error(
            `[env] VITE_API_BASE_URL must use http or https protocol. Received: "${url.protocol}".`
        );
    }

    return value.replace(/\/+$/, '');
}

export const env = {
    VITE_API_BASE_URL: normalizeBaseUrl(getRequiredEnv('VITE_API_BASE_URL')),
} as const;
