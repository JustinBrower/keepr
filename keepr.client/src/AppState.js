import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps: [],
  vaults: [],
  activeKeep: {},
  activeVault: {},
  allKeeps: [],
  vaultKeeps: [],
  activeProfile: {},
  myVaults: [],
})
