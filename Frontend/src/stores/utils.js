import { defineStore } from 'pinia'

const AUTH_TOKEN_KEYS = ['token_', 'token', 'jwt', 'authToken', 'user_token'];
const ROLE_ADMIN = 1;
const ROLE_USER = 2;

const decodeBase64Url = (value) => {
    const normalized = value.replace(/-/g, '+').replace(/_/g, '/');
    const padded = normalized.padEnd(Math.ceil(normalized.length / 4) * 4, '=');

    return decodeURIComponent(
        atob(padded)
            .split('')
            .map((character) => `%${character.charCodeAt(0).toString(16).padStart(2, '0')}`)
            .join('')
    );
};

export const getStoredToken = () => AUTH_TOKEN_KEYS
    .map((key) => localStorage.getItem(key))
    .find((token) => Boolean(token && token.trim()));

export const getJwtPayload = (token = getStoredToken()) => {
    if (!token) {
        return null;
    }

    const parts = token.split('.');

    if (parts.length < 2) {
        return null;
    }

    try {
        return JSON.parse(decodeBase64Url(parts[1]));
    } catch {
        return null;
    }
};

export const getCurrentUserRole = (token = getStoredToken()) => {
    const payload = getJwtPayload(token);

    if (payload) {
        const roleClaim = payload.role
            ?? payload.Role
            ?? payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
            ?? payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role'];

        if (roleClaim === 'Admin' || roleClaim === ROLE_ADMIN || roleClaim === String(ROLE_ADMIN)) {
            return ROLE_ADMIN;
        }

        if (roleClaim === 'User' || roleClaim === ROLE_USER || roleClaim === String(ROLE_USER)) {
            return ROLE_USER;
        }
    }

    const storedRole = Number(localStorage.getItem('user_role'));

    if (storedRole === ROLE_ADMIN || storedRole === ROLE_USER) {
        return storedRole;
    }

    return null;
};

export const isAdminUser = (token = getStoredToken()) => {
    const payload = getJwtPayload(token);

    if (!payload) {
        return false;
    }

    if (payload.exp && Date.now() >= payload.exp * 1000) {
        return false;
    }

    return getCurrentUserRole(token) === ROLE_ADMIN;
};

export const getAuthHeaders = (token = getStoredToken()) => {
    const cleanedToken = token?.trim();

    return cleanedToken ? { Authorization: `Bearer ${cleanedToken}` } : {};
};

export const storeAuthToken = (token) => {
    if (!token) {
        return;
    }

    localStorage.setItem('token_', token);
    localStorage.setItem('token', token);

    const role = getCurrentUserRole(token);

    if (role) {
        localStorage.setItem('user_role', String(role));
    }
};

export const useUtilsStore = defineStore('utils', ()=>
{
    // const url = "https://localhost:7163/api/"
    const url = "https://sae-401-cube-c0g9fue0cad9guey.germanywestcentral-01.azurewebsites.net/api/";
    return {url};
});